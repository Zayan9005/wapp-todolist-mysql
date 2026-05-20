using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class ContactModel : PageModel
{
    [BindProperty]
    public string FullName { get; set; } = "";

    [BindProperty]
    public string EmailAddress { get; set; } = "";

    [BindProperty]
    public string Subject { get; set; } = "";

    [BindProperty]
    public string Message { get; set; } = "";

    public string StatusMessage { get; set; } = "";

    public void OnGet() { }

    public void OnPost()
    {
        // Simulated successful submission
        StatusMessage = $"Thank you, {FullName}. Your message regarding '{Subject}' has been sent to our team.";
    }
}