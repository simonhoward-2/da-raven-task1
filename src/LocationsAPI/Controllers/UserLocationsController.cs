using LocationsAPI.DbContexts;
using LocationsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocationsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLocationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserLocationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLocation>>> GetAll()
        {
            return await _context.UserLocations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserLocation>> Get(int id)
        {
            var location = await _context.UserLocations.FindAsync(id);
            if (location == null)
                return NotFound();
            return location;
        }

        [HttpPost]
        public async Task<ActionResult<UserLocation>> Create(UserLocation location)
        {
            _context.UserLocations.Add(location);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = location.Id }, location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserLocation location)
        {
            if (id != location.Id)
                return BadRequest();
            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _context.UserLocations.FindAsync(id);
            if (location == null)
                return NotFound();
            _context.UserLocations.Remove(location);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
