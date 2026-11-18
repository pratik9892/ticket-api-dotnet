using TicketApi.Models;

namespace TicketApi.Services;

// Stores tickets in memory. Data is lost when the app stops.
public class TicketService : ITicketService
{
    private readonly List<Ticket> _tickets = new();
    private readonly object _lock = new(); // requests run on multiple threads; List<T> is not thread-safe
    private int _nextId = 1;

    public TicketService()
    {
        // Two sample tickets so GET returns something right away
        Create(new Ticket { Title = "Order not delivered", CustomerEmail = "riya@example.com", Priority = 2 });
        Create(new Ticket { Title = "Refund not received", CustomerEmail = "amit@example.com", Priority = 3 });
    }

    public IEnumerable<Ticket> GetAll()
    {
        lock (_lock)
        {
            return _tickets.ToList(); // return a copy, not the internal list
        }
    }

    public Ticket? GetById(int id)
    {
        lock (_lock)
        {
            return _tickets.FirstOrDefault(t => t.Id == id);
        }
    }

    public Ticket Create(Ticket ticket)
    {
        lock (_lock)
        {
            ticket.Id = _nextId++;
            ticket.CreatedAt = DateTime.UtcNow;
            _tickets.Add(ticket);
            return ticket;
        }
    }

    public bool Update(int id, Ticket updated)
    {
        lock (_lock)
        {
            var existing = _tickets.FirstOrDefault(t => t.Id == id);
            if (existing is null)
            {
                return false;
            }

            existing.Title = updated.Title;
            existing.CustomerEmail = updated.CustomerEmail;
            existing.Priority = updated.Priority;
            existing.Status = updated.Status;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var ticket = _tickets.FirstOrDefault(t => t.Id == id);
            return ticket is not null && _tickets.Remove(ticket);
        }
    }
}
