using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using LogisticApp.Models;

namespace LogisticApp.Pages;

public class IndexModel : PageModel
{
    public List<Item> Items { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);

            var response = await client.GetStringAsync("http://localhost:5178/messages");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Items = JsonSerializer.Deserialize<List<Item>>(response, options)
                    ?? new List<Item>();
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Could not reach the API: {ex.Message}";
        }
        catch (TaskCanceledException)
        {
            ErrorMessage = "Request timed out";
        }
    }
}