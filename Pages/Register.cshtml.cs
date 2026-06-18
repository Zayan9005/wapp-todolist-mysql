using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using BCrypt.Net;

namespace wapp.Pages;

public class RegisterModel : PageModel
{
    private readonly IConfiguration _configuration;

    public RegisterModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public string FullName { get; set; } = "";

    [BindProperty]
    public string Email { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    public string ErrorMessage { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "All verification details are mandatory fields.";
            return Page();
        }

        string connectionString = _configuration.GetConnectionString("TodoDb") ?? "";

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        // Ensure user registration is distinct across instances
        string checkSql = "SELECT COUNT(*) FROM todolist_users WHERE email = @email";
        using (MySqlCommand checkCmd = new MySqlCommand(checkSql, connection))
        {
            checkCmd.Parameters.AddWithValue("@email", Email);
            long count = (long)checkCmd.ExecuteScalar();
            if (count > 0)
            {
                ErrorMessage = "An account linked with this email already exists.";
                return Page();
            }
        }

        string hash = BCrypt.Net.BCrypt.HashPassword(Password);

        string sql = "INSERT INTO todolist_users (full_name, email, password_hash) VALUES (@full_name, @email, @password_hash)";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@full_name", FullName);
        command.Parameters.AddWithValue("@email", Email);
        command.Parameters.AddWithValue("@password_hash", hash);

        command.ExecuteNonQuery();

        return RedirectToPage("/Login");
    }
}