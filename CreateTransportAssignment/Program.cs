using CreateTransportAssignment.Models;
using CreateTransportAssignment.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MessageStore>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// POST endpoint
app.MapPost("/messages", (Message message, MessageStore store) =>
{
    var receivedAt = DateTime.UtcNow;

    var stamped = message with
    {
        Id = store.Messages.Count + 1,
        ReceivedAt = receivedAt
    };

    store.Messages.Add(stamped);

    Console.WriteLine($"[RECEIVED] Id={stamped.Id} | SentAt={stamped.SentAt:O} | ReceivedAt={receivedAt:O}");

    return Results.Ok(new
    {
        status = "received",
        id = message.Id,
        sentAt = stamped.SentAt,
        receivedAt = receivedAt
    });
});

app.MapGet("/messages", (MessageStore store) => store.Messages);

app.Run();