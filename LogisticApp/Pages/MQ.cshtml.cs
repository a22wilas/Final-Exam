using Microsoft.AspNetCore.Mvc.RazorPages;
using LogisticApp.Models;

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
    }
}