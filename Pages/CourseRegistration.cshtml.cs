using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class CourseFee
{
    public string CourseName { get; set; } = "";
    public double FullTimeFee { get; set; }
    public double PartTimeFee { get; set; }
    public double OnlineFee { get; set; }
}

public class CourseRegistrationModel : PageModel
{
    private readonly List<CourseFee> CourseList = new()
    {
        new CourseFee { CourseName = "Web Applications", FullTimeFee = 2500, PartTimeFee = 1800, OnlineFee = 1500 },
        new CourseFee { CourseName = "Database Systems", FullTimeFee = 2300, PartTimeFee = 1700, OnlineFee = 1400 },
        new CourseFee { CourseName = "Programming", FullTimeFee = 2200, PartTimeFee = 1600, OnlineFee = 1300 }
    };

    [BindProperty] public string FullName { get; set; } = "";
    [BindProperty] public string StudentId { get; set; } = "";
    [BindProperty] public string Programme { get; set; } = "";
    [BindProperty] public string SelectedCourse { get; set; } = "";
    [BindProperty] public string StudyMode { get; set; } = "";
    [BindProperty] public string PaymentMethod { get; set; } = "";

    public string ConfirmationMessage { get; set; } = "";
    public string ModeMessage { get; set; } = "";
    public double OriginalFee { get; set; }
    public double DiscountRate { get; set; }
    public double DiscountAmount { get; set; }
    public double FinalFee { get; set; }

    public void OnPost()
    {
        ConfirmationMessage = "Registration submitted successfully.";

        ModeMessage = StudyMode switch
        {
            "Full-Time" => "You have selected full-time study.",
            "Part-Time" => "You have selected part-time study.",
            "Online" => "You have selected online study.",
            _ => ""
        };

        var course = CourseList.FirstOrDefault(c => c.CourseName == SelectedCourse);
        if (course != null)
        {
            OriginalFee = StudyMode switch
            {
                "Full-Time" => course.FullTimeFee,
                "Part-Time" => course.PartTimeFee,
                "Online" => course.OnlineFee,
                _ => 0
            };
        }

        double rate = 0;
        if (PaymentMethod == "Full Payment") rate += 10;
        if (StudyMode == "Online") rate += 5;
        else if (StudyMode == "Part-Time") rate += 3;

        DiscountRate = rate;
        DiscountAmount = OriginalFee * (DiscountRate / 100);
        FinalFee = OriginalFee - DiscountAmount;
    }
}