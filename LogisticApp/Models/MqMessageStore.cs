using System.Collections.Concurrent;

namespace LogisticApp.Models
{
    public class MqMessageStore
    {
        public ConcurrentBag<Item> Items { get; } = new();
    }
}