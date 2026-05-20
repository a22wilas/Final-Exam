using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
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

    public async Task<IActionResult> OnGetDownloadCsvAsync()
    {
        
        try
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);

            var response = await client.GetStringAsync("http://localhost:5178/messages");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Items = JsonSerializer.Deserialize<List<Item>>(response, options) ?? new();
        }
        catch
        {
            Items = new();
        }

        var sb = new StringBuilder();

           sb.AppendLine("Id,AuthorityCodeField,AssignmentIdField,AssignmentReferenceIdField," +
                      "Package,PickupLocationField,DeliveryLocationField,PriorityField," +
                      "SentAt,ReceivedAt,Latency(ms)");

        foreach (var item in Items)
        {
            var latency = (item.SentAt.HasValue && item.ReceivedAt.HasValue)
                ? (item.ReceivedAt.Value - item.SentAt.Value).TotalMilliseconds.ToString("F2")
                : "N/A";

            sb.AppendLine(
                $"\"{item.Id}\"," +
                $"\"{item.AuthorityCodeField}\"," +
                $"\"{item.AssignmentIdField}\"," +
                $"\"{item.AssignmentReferenceIdField}\"," +
                $"\"{item.Package}\"," +
                $"\"{item.PickupLocationField}\"," +
                $"\"{item.DeliveryLocationField}\"," +
                $"\"{item.PriorityField}\"," +
                $"\"{item.SentAt?.ToString("yyyy-MM-dd HH:mm:ss.fff")}\"," +
                $"\"{item.ReceivedAt?.ToString("yyyy-MM-dd HH:mm:ss.fff")}\"," +
                $"\"{latency}\""
            );
        }

        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        return File(bytes, "text/csv", "API_logistic_results.csv");
    }
}