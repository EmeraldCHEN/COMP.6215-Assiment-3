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
    public class MoviesController : ControllerBase
    {
        private readonly DB_6215_19_S1Context _context;
        public MoviesController(DB_6215_19_S1Context context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET single api/value
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovies(int id)
        {   
            Movies item = await _context.Movies.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

       // POST api/values
        [HttpPost]
        public async Task<ActionResult<Movies>> PostMovies(Movies item)
        {
            _context.Movies.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
              nameof(GetMovies), 
              item);
        }

      //  PUT single api/value
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoviesItem(int id, Movies item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Content("MODEL has been updated");
        }

        // DELETE single api/value
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteMoviesItem(int id)
        // {
        //     Movies model = await _context.Movies.FindAsync(id);

        //     if (model == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Movies.Remove(model);
        //     await _context.SaveChangesAsync();

        //     return Content("Model has been removed");
        // }
    }
}
