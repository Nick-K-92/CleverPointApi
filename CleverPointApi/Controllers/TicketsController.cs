using CleverPointApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleverPointApi.Controllers
{
    [ApiController]
    [Route("Tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly CleverPointDbContext _context;

        public TicketsController(CleverPointDbContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public IActionResult GetTickets()
        {
            var tickets = _context.Tickets.AsNoTracking().ToList();

            int count = tickets.Count();

            return Ok(new { data = tickets, count });
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Ticket? ticket = await _context.Tickets.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket([FromRoute] int id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != ticket.Id)
                return BadRequest();

            try
            {
                var sameShipmentTicket = _context.Tickets.Any(t => t.Id != ticket.Id && t.ShipmentID == ticket.ShipmentID);
                if (sameShipmentTicket)
                    throw new Exception("There is already a ticket with this ShipmentId");

                _context.Entry(ticket).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }

            return Ok(ticket);
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var sameShipmentTicket = _context.Tickets.Any(t => t.ShipmentID == ticket.ShipmentID);
                if (sameShipmentTicket)
                    throw new Exception("There is already a ticket with this ShipmentId");

                if (ticket.CreatorUserId == 0)
                    throw new Exception("The creator user of the Ticket cannot be empty");

                if (ticket.StatusId == 0)
                {
                    Status status = _context.Status.AsNoTracking().Single(s => s.IsFirstStatusOfTicket);
                    ticket.StatusId = status.Id;
                }

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Ticket? ticket = await _context.Tickets.SingleOrDefaultAsync(m => m.Id == id);
                if (ticket == null)
                    return NotFound();

                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();

                return Ok(ticket);
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }
        }

        // Search: api/SearchTicket
        [HttpPost]
        [Route("SearchTickets")]
        public async Task<IActionResult> SearchTickets([FromBody] TicketFilters ticketFilters)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                List<Ticket> resultTickets = new List<Ticket>();

                var tickets = _context.Tickets
                    .AsNoTracking()
                    .Include(t => t.CreatorUser)
                    .Include(t => t.AssigneeUser);

                if (!String.IsNullOrEmpty(ticketFilters.SearchQuery))
                {
                    string searchQuery = ticketFilters.SearchQuery.Trim();

                    resultTickets.AddRange(
                        await tickets.Where(t =>
                            t.Title.Contains(searchQuery) ||
                            (t.Description != null && t.Description.Contains(searchQuery)) ||
                            (t.AssigneeUser != null && t.AssigneeUser.Username.Contains(searchQuery)) ||
                            (t.CreatorUser != null && t.CreatorUser.Username.Contains(searchQuery)) ||
                            t.ShipmentID.Contains(searchQuery) ||
                            t.ShipmentTrackingNumber.Contains(searchQuery)
                        )
                        .ToListAsync());
                }

                if (ticketFilters.DateOfCreation != null)
                {
                    resultTickets.AddRange(
                        await tickets.Where(t => t.DateCreated.Date == ticketFilters.DateOfCreation).ToListAsync()
                       );
                }

                if (ticketFilters.StoryPoints != null)
                {
                    resultTickets.AddRange(
                        await tickets.Where(t => t.EstimatedStoryPoints == ticketFilters.StoryPoints).ToListAsync()
                       );
                }

                if (ticketFilters.StatusId != null)
                {
                    resultTickets.AddRange(
                        await tickets.Where(t => t.StatusId == ticketFilters.StatusId).ToListAsync()
                       );
                }

                return Ok(resultTickets);
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }
        }

        // AssignTicket: api/AssignTicket/{ticketId}/{assigneeId}
        [HttpGet]
        [Route("AssignTicket/{ticketId}/{assigneeId}")]
        public async Task<IActionResult> AssignTicket([FromRoute] int ticketId, [FromRoute] int assigneeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Ticket? ticket = await _context.Tickets.SingleOrDefaultAsync(t => t.Id == ticketId);

                if (ticket == null)
                    return NotFound();

                ticket.AssigneeUserId = assigneeId;

                await _context.SaveChangesAsync();

                return Ok(ticket);
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }
        }

        // ChangeStatus: api/ChangeStatus/{ticketId}/{statusId}
        [HttpGet]
        [Route("ChangeStatus/{ticketId}/{statusId}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int ticketId, [FromRoute] int statusId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Ticket? ticket = await _context.Tickets.SingleOrDefaultAsync(t => t.Id == ticketId);

                if (ticket == null)
                    return NotFound();

                ticket.StatusId = statusId;

                await _context.SaveChangesAsync();

                return Ok(ticket);
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }
        }
    }
}
