using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class GradeCalculatorModel : PageModel
{
    // [BindProperty] MUST be here, outside the methods
    [BindProperty]
    public string StudentName { get; set; } = "";

    [BindProperty]
    public int Grade { get; set; }

    // This property doesn't need binding as it's for output only
    public string ResultMessage { get; set; } = "";
    public string GradeCategory { get; set; } = "";
    public string StatusClass { get; set; } = "";

    public void OnGet()
    {
    }

    public void OnPost()
    {
        // DO NOT put [BindProperty] inside here!
        if (Grade >= 75)
        {
            GradeCategory = "Distinction";
            ResultMessage = $"Excellent work, {StudentName}! You've achieved a Distinction.";
            StatusClass = "distinction";
        }
        else if (Grade >= 65)
        {
            GradeCategory = "Merit";
            ResultMessage = $"Great job, {StudentName}! You've achieved a Merit.";
            StatusClass = "merit";
        }
        else if (Grade >= 50)
        {
            GradeCategory = "Pass";
            ResultMessage = $"Congratulations, {StudentName}! You have passed.";
            StatusClass = "pass";
        }
        else
        {
            GradeCategory = "Fail";
            ResultMessage = $"Sorry, {StudentName}. You have failed this assessment.";
            StatusClass = "fail";
        }
    }
}