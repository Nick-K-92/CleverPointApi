using CleverPointApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleverPointApi.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly CleverPointDbContext _context;

        public UsersController(CleverPointDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.AsNoTracking().ToList();

            int count = users.Count();

            return Ok(new { data = users, count });
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User? user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id)
                return BadRequest();

            try
            {
                var sameUsernameUser = _context.Users.Any(u => u.Id != user.Id && u.Username == user.Username);
                if (sameUsernameUser)
                    throw new Exception("There is already a User with this Username");

                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var sameUsernameUser = _context.Users.Any(u => u.Username == user.Username);
                if (sameUsernameUser)
                    throw new Exception("There is already a User with this Username");

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                User? user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
                if (user == null)
                    return NotFound();

                bool userIsAssociatedWithTickets = _context.Tickets.Any(t => t.CreatorUserId == id || t.AssigneeUserId == id);
                if (userIsAssociatedWithTickets)
                    throw new Exception("This User is associated with Tickets and cannot be deleted");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(ControllerTools.CreateErrorResponse(e));
            }
        }
    }
}
