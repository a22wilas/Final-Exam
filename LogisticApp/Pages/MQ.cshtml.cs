using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LogisticApp.Models;
using System.Text;

namespace LogisticApp.Pages
{
    public class MQModel : PageModel
    {
        private readonly MqMessageStore _store;

        public MQModel(MqMessageStore store)
        {
            _store = store;
        }

        public List<Item> Items { get; set; } = new();

        public void OnGet()
        {
            Items = _store.Items.ToList();
        }

        public IActionResult OnGetDownloadCsv()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("Id,AuthorityCodeField,AssignmentIdField,AssignmentReferenceIdField," +
                            "Package,PickupLocationField,DeliveryLocationField,PriorityField," +
                            "SentAt,ReceivedAt,Latency(ms)");
            
                            

            foreach (var item in _store.Items)
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
            return File(bytes, "text/csv", "MQ_logistic_results.csv");
        }
    }
}