using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wapp.Pages;

public class CoursesModel : PageModel
{
    public string CurrentModule { get; set; } = "Web Applications"; //
    
    public List<ModuleDetail> Modules { get; set; } = new()
    {
        new ModuleDetail { 
            Title = "HTML Fundamentals", 
            Description = "Defining the content and structure of web pages.", //
            Icon = "🌐",
            Progress = 100
        },
        new ModuleDetail { 
            Title = "Semantic HTML", 
            Description = "Using elements to organize page content clearly.", //
            Icon = "🏗️",
            Progress = 90
        },
        new ModuleDetail { 
            Title = "Razor Pages", 
            Description = "Allowing HTML and C# to work together in .cshtml files.", //
            Icon = "⚡",
            Progress = 75
        }
    };
}

public class ModuleDetail
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Icon { get; set; } = "";
    public int Progress { get; set; }
}