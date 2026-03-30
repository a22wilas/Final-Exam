using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogisticApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string? Name { get; set; }
    
    public void OnGet()
    {
        
    }

    // Runs when form is submitted
    public void OnPost()
    {
        
    }
}
