using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MySqlConnector;

namespace wapp.Pages;

public class LoginModel : PageModel
{
    private readonly IConfiguration _configuration;

    public LoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public string Email { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    public string Message { get; set; } = "";

    public void OnGet() { }

    public IActionResult OnPost()
    {
        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT id, full_name, password_hash FROM todolist_users WHERE email = @email";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@email", Email);

        using MySqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            int userId = Convert.ToInt32(reader["id"]);
            string fullName = reader["full_name"].ToString() ?? "";
            string storedHash = reader["password_hash"].ToString() ?? "";

            bool passwordCorrect = BCrypt.Net.BCrypt.Verify(Password, storedHash);
            if (passwordCorrect)
            {
                HttpContext.Session.SetInt32("UserId", userId);
                HttpContext.Session.SetString("FullName", fullName);
                return RedirectToPage("/TodoList");
            }
        }

        Message = "Invalid email address or password.";
        return Page();
    }
}