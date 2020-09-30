using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms.ar.xarchitecture.de.cmsDatabase;

namespace cms.ar.xarchitecture.de.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudiesAPIController : ControllerBase
    {
        private readonly cmsDatabaseContext _context;

        public StudiesAPIController(cmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StudiesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studies>>> GetStudies()
        {
            return await _context.Studies.ToListAsync();
        }

        // GET: api/StudiesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Studies>> GetStudies(int id)
        {
            var studies = await _context.Studies.FindAsync(id);

            if (studies == null)
            {
                return NotFound();
            }

            return studies;
        }

        // PUT: api/StudiesAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudies(int id, Studies studies)
        {
            if (id != studies.Id)
            {
                return BadRequest();
            }

            _context.Entry(studies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudiesExists(id))
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

        // POST: api/StudiesAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Studies>> PostStudies(Studies studies)
        {
            _context.Studies.Add(studies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudies", new { id = studies.Id }, studies);
        }

        // DELETE: api/StudiesAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Studies>> DeleteStudies(int id)
        {
            var studies = await _context.Studies.FindAsync(id);
            if (studies == null)
            {
                return NotFound();
            }

            _context.Studies.Remove(studies);
            await _context.SaveChangesAsync();

            return studies;
        }

        private bool StudiesExists(int id)
        {
            return _context.Studies.Any(e => e.Id == id);
        }
    }
}
