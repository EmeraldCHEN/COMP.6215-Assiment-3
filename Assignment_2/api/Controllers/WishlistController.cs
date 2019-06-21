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
    public class WishlistController : ControllerBase
    {
        private readonly DB_6215_19_S1Context _context;
        public WishlistController(DB_6215_19_S1Context context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlist()
        {
            return await _context.Wishlist.ToListAsync();
        }

        // GET single api/value
        [HttpGet("{id}")]
        public async Task<ActionResult<Wishlist>> GetWishlist(long id)
        {   
            Wishlist item = await _context.Wishlist.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Wishlist>> PostWishlist(Wishlist item)
        {
            _context.Wishlist.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
              nameof(GetWishlist), 
              item);
        }

        // PUT single api/value
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishlistItem(int id, Wishlist item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Content("Wishlist has been updated");
        }

        // DELETE single api/value
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlistItem(int id)
        {
            Wishlist model = await _context.Wishlist.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            _context.Wishlist.Remove(model);
            await _context.SaveChangesAsync();

            return Content("Wishlist has been removed");
        }
    }
}
