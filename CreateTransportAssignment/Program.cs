using CreateTransportAssignment.Models;
using CreateTransportAssignment.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MessageStore>();

// Add OpenAPI (Swagger)
builder.Services.AddOpenApi();

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

// POST endpoint to receive messages
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




