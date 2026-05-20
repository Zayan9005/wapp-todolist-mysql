using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class MyCVModel : PageModel
{
    public string FullName { get; set; } = "Zayan Aijaz Reshi";
    public string Objective { get; set; } = "Aspiring Software Engineer focused on building scalable web architectures and efficient system solutions.";
    
    // Data for the interactive skill matrix
    public List<Skill> TechnicalSkills { get; set; } = new()
    {
        new Skill { Name = "Python", Level = 95, Color = "#3776ab" },
        new Skill { Name = "Java", Level = 88, Color = "#f8981d" },
        new Skill { Name = "C++", Level = 82, Color = "#00599c" },
        new Skill { Name = "HTML/Razor", Level = 98, Color = "#e34c26" }
    };
}

public class Skill
{
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public string Color { get; set; } = "";
}