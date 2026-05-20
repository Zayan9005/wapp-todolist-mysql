using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class FeedbackFormModel : PageModel
{
    // [BindProperty] must be outside the OnPost method to avoid CS7014
    [BindProperty] 
    public string FullName { get; set; } = "";

    [BindProperty] 
    public string ModuleName { get; set; } = "";

    [BindProperty] 
    public string Feedback { get; set; } = "";

    public string ConfirmationMessage { get; set; } = "";

    public void OnGet()
    {
    }

    public void OnPost()
    {
        // Logic to generate confirmation message after submission
        ConfirmationMessage = $"Thank you, {FullName}. Your feedback for {ModuleName} has been received.";
    }
}