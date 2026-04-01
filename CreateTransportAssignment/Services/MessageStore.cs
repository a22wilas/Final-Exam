using CreateTransportAssignment.Models;

namespace CreateTransportAssignment.Services;

public class MessageStore
{
    public List<Message> Messages { get; } = new();
}