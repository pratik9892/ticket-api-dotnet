using Microsoft.AspNetCore.Mvc;
using TicketApi.Models;
using TicketApi.Services;

namespace TicketApi.Controllers;

[ApiController]                 // automatic 400 Bad Request when validation fails
[Route("api/[controller]")]     // base URL: /api/tickets
public class TicketsController : ControllerBase
{
    private readonly ITicketService _service;

    // ASP.NET Core injects the registered ITicketService here (constructor injection)
    public TicketsController(ITicketService service)
    {
        _service = service;
    }

    // GET /api/tickets
    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    // GET /api/tickets/1
    [HttpGet("{id:int}")]
    public ActionResult<Ticket> GetById(int id)
    {
        var ticket = _service.GetById(id);
        if (ticket is null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }

    // POST /api/tickets
    [HttpPost]
    public ActionResult<Ticket> Create(Ticket ticket)
    {
        var created = _service.Create(ticket);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created); // 201 + Location header
    }

    // PUT /api/tickets/1
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Ticket ticket)
    {
        if (!_service.Update(id, ticket))
        {
            return NotFound();
        }
        return NoContent(); // 204
    }

    // DELETE /api/tickets/1
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!_service.Delete(id))
        {
            return NotFound();
        }
        return NoContent(); // 204
    }
}
