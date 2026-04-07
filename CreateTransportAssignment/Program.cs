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

    store.Messages.Add(message);

    Console.WriteLine($"[RECEIVED] Id={message.Id}, Package={message.Package}, Time={receivedAt}");

    return Results.Ok(new
    {
        status = "received",
        id = message.Id,
        receivedAt = receivedAt
    });
});

app.MapGet("/messages", (MessageStore store) =>
{
    return store.Messages;
});

app.Run();

// Data model for incoming JSON


