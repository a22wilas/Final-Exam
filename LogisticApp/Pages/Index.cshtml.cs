using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using LogisticApp.Models;

namespace LogisticApp.Pages;

public class IndexModel : PageModel
{
    public List<Item> Items { get; set; } = new();

    public async Task OnGetAsync()
{
    using var client = new HttpClient();

    var response = await client.GetStringAsync("http://localhost:5178/messages");

    Items = JsonSerializer.Deserialize<List<Item>>(response) ?? new List<Item>();
}
}