using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public string Message { get; set; } = "";
    public List<TodoItem> Todos { get; set; } = new List<TodoItem>();

    public void OnGet()
    {
        LoadTodoItems();
    }

    public void OnPost()
    {
        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "INSERT INTO todolist (task, completed, due_date) VALUES (@task, @completed, @due_date)";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@task", Task);
        command.Parameters.AddWithValue("@completed", Completed);
        command.Parameters.AddWithValue("@due_date", DueDate.HasValue ? (object)DueDate.Value.Date : DBNull.Value);
        
        command.ExecuteNonQuery();
        Message = "New task added successfully.";
        LoadTodoItems();
    }

    public IActionResult OnGetToggleStatus(int id)
    {
        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "UPDATE todolist SET completed = NOT completed WHERE id = @id";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();

        return RedirectToPage();
    }

    // EXERCISE: Added Delete Handler Method
    public IActionResult OnGetDelete(int id)
    {
        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "DELETE FROM todolist WHERE id = @id";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();

        return RedirectToPage();
    }

    private void LoadTodoItems()
    {
        Todos.Clear();
        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT id, task, completed, due_date FROM todolist ORDER BY id DESC";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            TodoItem item = new TodoItem();
            item.Id = Convert.ToInt32(reader["id"]);
            item.Task = reader["task"].ToString() ?? "";
            item.Completed = Convert.ToBoolean(reader["completed"]);
            
            if (reader["due_date"] == DBNull.Value)
            {
                item.DueDate = null;
            }
            else
            {
                item.DueDate = Convert.ToDateTime(reader["due_date"]);
            }
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