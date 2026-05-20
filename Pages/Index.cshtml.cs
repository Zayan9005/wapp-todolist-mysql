using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class IndexModel : PageModel
{
    public string StudentName { get; set; } = "Zayan"; //
    public string ModuleName { get; set; } = "Web Applications"; //

    public void OnGet()
    {
        // Logic to run when the home page loads
    }
}