using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class StudentInfoModel : PageModel
{
    // Real data from your profile
    public string Name { get; set; } = "Zayan Aijaz Reshi"; 
    public string StudentID { get; set; } = "TP087076"; 
    public string Programme { get; set; } = "Web Applications"; 
    public string Email { get; set; } = "zayan9005@gmail.com";
    
    // Interactive element
    public string LastLogin { get; set; } = "";

    public void OnGet()
    {
        LastLogin = DateTime.Now.ToString("dd MMM yyyy, hh:mm tt");
    }
}