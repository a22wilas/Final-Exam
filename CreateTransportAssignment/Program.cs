var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI (Swagger)
builder.Services.AddOpenApi();

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// POST endpoint to receive messages
app.MapPost("/messages", (Message message) =>
{
    var receivedAt = DateTime.UtcNow;

    Console.WriteLine($"[RECEIVED] Id={message.Id}, Package={message.Package}, Assignment Id={message.AssignmentIdField}, Time={receivedAt}");

    return Results.Ok(new
    {
        status = "received",
        id = message.Id,
        receivedAt = receivedAt
    });
});

app.Run();

// Data model for incoming JSON
record Message(
    int Id,
    string AuthorityCodeField,
    string AssignmentIdField,
    string AssignmentReferenceIdField,
    string Package,
    string PickupLocationField,
    string DeliveryLocationField,
    string PriorityField
);

