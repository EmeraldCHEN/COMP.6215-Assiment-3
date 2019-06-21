using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DB_6215_19_S1Context _context;
        public UsersController(DB_6215_19_S1Context context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET single api/value
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {   
            Users item = await _context.Users.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
              nameof(GetUsers), 
              item);
        }

        // PUT single api/value
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersItem(int id, Users item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Content("User has been updated");
        }

        // DELETE single api/value
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersItem(int id)
        {
            Users user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Content("User has been removed");
        }
    }
}
