using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MySqlConnector;

namespace wapp.Pages;

public class TodoListModel : PageModel
{
    private readonly IConfiguration _configuration;

    public TodoListModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public string Task { get; set; } = "";

    [BindProperty]
    public bool Completed { get; set; }

    [BindProperty]
    public DateTime? DueDate { get; set; }

    [BindProperty]
    public int EditId { get; set; }

    [BindProperty]
    public string EditTask { get; set; } = "";

    [BindProperty]
    public bool EditCompleted { get; set; }

    [BindProperty]
    public DateTime? EditDueDate { get; set; }

    public string Message { get; set; } = "";

    public List<TodoItem> Todos { get; set; } = new List<TodoItem>();

    public IActionResult OnGet()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        LoadTodoItems(userId.Value);
        return Page();
    }

    public IActionResult OnPost()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "INSERT INTO todolist (task, completed, due_date, user_id) VALUES (@task, @completed, @due_date, @user_id)";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@task", Task);
        command.Parameters.AddWithValue("@completed", Completed);
        command.Parameters.AddWithValue("@due_date", DueDate.HasValue ? (object)DueDate.Value.Date : DBNull.Value);
        command.Parameters.AddWithValue("@user_id", userId.Value);

        command.ExecuteNonQuery();

        Message = "New task added successfully.";
        LoadTodoItems(userId.Value);
        return Page();
    }

    public IActionResult OnPostEdit()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = @"UPDATE todolist
                       SET task = @task,
                           completed = @completed,
                           due_date = @due_date
                       WHERE id = @id AND user_id = @user_id";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@task", EditTask);
        command.Parameters.AddWithValue("@completed", EditCompleted);
        command.Parameters.AddWithValue("@due_date", EditDueDate.HasValue ? (object)EditDueDate.Value.Date : DBNull.Value);
        command.Parameters.AddWithValue("@id", EditId);
        command.Parameters.AddWithValue("@user_id", userId.Value);

        command.ExecuteNonQuery();

        return RedirectToPage();
    }

    public IActionResult OnGetToggleStatus(int id)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "UPDATE todolist SET completed = NOT completed WHERE id = @id AND user_id = @user_id";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@user_id", userId.Value);
        command.ExecuteNonQuery();

        return RedirectToPage();
    }

    public IActionResult OnGetDelete(int id)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "DELETE FROM todolist WHERE id = @id AND user_id = @user_id";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@user_id", userId.Value);
        command.ExecuteNonQuery();

        return RedirectToPage();
    }

    private void LoadTodoItems(int userId)
    {
        Todos.Clear();
        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT id, task, completed, due_date FROM todolist WHERE user_id = @user_id ORDER BY id DESC";

        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@user_id", userId);
        
        using MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            TodoItem item = new TodoItem();
            item.Id = Convert.ToInt32(reader["id"]);
            item.Task = reader["task"].ToString() ?? "";
            item.Completed = Convert.ToBoolean(reader["completed"]);
            item.DueDate = reader["due_date"] == DBNull.Value ? null : Convert.ToDateTime(reader["due_date"]);

            Todos.Add(item);
        }
    }
}

public class TodoItem
{
    public int Id { get; set; }
    public string Task { get; set; } = "";
    public bool Completed { get; set; }
    public DateTime? DueDate { get; set; }
}