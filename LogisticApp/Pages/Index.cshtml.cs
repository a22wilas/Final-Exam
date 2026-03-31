using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using LogisticApp.Models;

namespace LogisticApp.Pages;

public class IndexModel : PageModel
{
    public List<Item> Items { get; set; } = new();

    public void OnGet()
    {
        var json = System.IO.File.ReadAllText("TRPItems.json");
        Items = JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
    }
}