# Ticket API (.NET)

A REST API for managing customer support tickets, built with **C#** and **ASP.NET Core Web API**.
It supports full CRUD (create, read, update, delete) with input validation and correct HTTP status codes.

## Tech stack
- C# / .NET 10
- ASP.NET Core Web API (controllers)
- Built-in dependency injection
- Data annotations for validation
- In-memory storage (thread-safe)

## Project structure
```
TicketApi/
├── Controllers/TicketsController.cs   # HTTP endpoints
├── Services/ITicketService.cs         # interface (abstraction)
├── Services/TicketService.cs          # in-memory implementation
├── Models/Ticket.cs                   # Ticket model + validation rules
└── Program.cs                         # app setup + DI registration
```

Request flow: `Client → TicketsController → ITicketService → TicketService → in-memory list`

## Run it
Requires the .NET 10 SDK.

```bash
cd TicketApi
dotnet run
```

The API runs at `http://localhost:5080`.

## Endpoints

| Method | URL | Description | Success |
|---|---|---|---|
| GET | /api/tickets | List all tickets | 200 |
| GET | /api/tickets/{id} | Get one ticket | 200 / 404 |
| POST | /api/tickets | Create a ticket | 201 |
| PUT | /api/tickets/{id} | Update a ticket | 204 / 404 |
| DELETE | /api/tickets/{id} | Delete a ticket | 204 / 404 |

Invalid input (empty title, bad email, priority outside 1-5) returns **400 Bad Request**.

Example body:
```json
{
  "title": "Damaged item received",
  "customerEmail": "neha@example.com",
  "priority": 1,
  "status": "Open"
}
```

## Testing
- Import `TicketApi.postman_collection.json` into Postman, or
- Open `TicketApi/TicketApi.http` in VS Code / Visual Studio and click "Send Request".

## Design notes
- The controller depends on the `ITicketService` interface, not the concrete class, so the in-memory store can be replaced with a database (e.g. EF Core + PostgreSQL) without changing the controller.
- `TicketService` is registered as a **singleton** so the data survives between requests.
- A `lock` protects the list because ASP.NET Core handles requests on multiple threads.

## Next steps
- Replace in-memory storage with EF Core + PostgreSQL
- Add DTOs so clients cannot set `Id` or `CreatedAt`
- Add unit tests with xUnit
