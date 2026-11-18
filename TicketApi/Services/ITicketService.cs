using TicketApi.Models;

namespace TicketApi.Services;

// The controller depends on this interface, not on the concrete class.
// That lets us swap the in-memory store for a database later without touching the controller.
public interface ITicketService
{
    IEnumerable<Ticket> GetAll();
    Ticket? GetById(int id);
    Ticket Create(Ticket ticket);
    bool Update(int id, Ticket ticket);
    bool Delete(int id);
}
