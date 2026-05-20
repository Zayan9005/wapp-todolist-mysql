using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class ProfileModel : PageModel
{
    [BindProperty]
    public string StudentName { get; set; } = "Zayan";

    [BindProperty]
    public string TPNumber { get; set; } = "TP087076";

    [BindProperty]
    public string Bio { get; set; } = "Computer Science Student @ APU | Web Applications Enthusiast";

    public void OnGet()
    {
    }
}