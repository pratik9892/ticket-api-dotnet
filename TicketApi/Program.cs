using System.Text.Json.Serialization;
using TicketApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers + send/receive enums as text ("Open") instead of numbers (0)
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Dependency injection: whenever a class asks for ITicketService,
// give it the same single TicketService instance (so data survives between requests)
builder.Services.AddSingleton<ITicketService, TicketService>();

var app = builder.Build();

app.MapGet("/", () => "Ticket API is running. Try GET /api/tickets");
app.MapControllers();

app.Run();
