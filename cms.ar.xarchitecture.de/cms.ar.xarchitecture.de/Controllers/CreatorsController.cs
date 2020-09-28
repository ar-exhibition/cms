using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms.ar.xarchitecture.de.cmsDatabase;

namespace cms.ar.xarchitecture.de.Controllers
{
    public class CreatorsController : Controller
    {
        private readonly cmsDatabaseContext _context;

        public CreatorsController(cmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Creators
        public async Task<IActionResult> Index()
        {
            var cmsDatabaseContext = _context.Creator.Include(c => c.StudiesNavigation);
            return View(await cmsDatabaseContext.ToListAsync());
        }

        // GET: Creators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creator = await _context.Creator
                .Include(c => c.StudiesNavigation)
                .FirstOrDefaultAsync(m => m.CreatorId == id);
            if (creator == null)
            {
                return NotFound();
            }

            return View(creator);
        }

        // GET: Creators/Create
        public IActionResult Create()
        {
            ViewData["Studies"] = new SelectList(_context.Studies, "Studies1", "Studies1");
            return View();
        }

        // POST: Creators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatorId,Name,Studies")] Creator creator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Studies"] = new SelectList(_context.Studies, "Studies1", "Studies1", creator.Studies);
            return View(creator);
        }

        // GET: Creators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creator = await _context.Creator.FindAsync(id);
            if (creator == null)
            {
                return NotFound();
            }
            ViewData["Studies"] = new SelectList(_context.Studies, "Studies1", "Studies1", creator.Studies);
            return View(creator);
        }

        // POST: Creators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreatorId,Name,Studies")] Creator creator)
        {
            if (id != creator.CreatorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreatorExists(creator.CreatorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Studies"] = new SelectList(_context.Studies, "Studies1", "Studies1", creator.Studies);
            return View(creator);
        }

        // GET: Creators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creator = await _context.Creator
                .Include(c => c.StudiesNavigation)
                .FirstOrDefaultAsync(m => m.CreatorId == id);
            if (creator == null)
            {
                return NotFound();
            }

            return View(creator);
        }

        // POST: Creators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creator = await _context.Creator.FindAsync(id);
            _context.Creator.Remove(creator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreatorExists(int id)
        {
            return _context.Creator.Any(e => e.CreatorId == id);
        }
    }
}
