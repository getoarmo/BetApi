using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetApi.Models;

namespace BetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetItemsController : ControllerBase
    {
        private readonly BetApiDbContext _context;

        public BetItemsController(BetApiDbContext context)
        {
            _context = context;
        }

        // GET: api/BetItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BetItem>>> GetBetItems()
        {
            return await _context.BetItems.ToListAsync();
        }

        // GET: api/BetItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BetItem>> GetBetItem(int id)
        {
            var betItem = await _context.BetItems.FindAsync(id);

            if (betItem == null)
            {
                return NotFound();
            }

            return betItem;
        }

        // PUT: api/BetItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBetItem(int id, BetItem betItem)
        {
            if (id != betItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(betItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBetItem", new { id = betItem.Id }, betItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BetItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BetItem>> PostBetItem(BetItem betItem)
        {
            _context.BetItems.Add(betItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBetItem", new { id = betItem.Id }, betItem);
        }

        // DELETE: api/BetItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBetItem(int id)
        {
            var betItem = await _context.BetItems.FindAsync(id);
            if (betItem == null)
            {
                return NotFound();
            }

            _context.BetItems.Remove(betItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BetItemExists(int id)
        {
            return _context.BetItems.Any(e => e.Id == id);
        }
    }
}
