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
    public class AnchorsController : Controller
    {
        private readonly cmsDatabaseContext _context;

        public AnchorsController(cmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Anchors
        public async Task<IActionResult> Index()
        {
            var cmsDatabaseContext = _context.Anchor.Include(a => a.Asset).Include(a => a.Scene);
            return View(await cmsDatabaseContext.ToListAsync());
        }

        // GET: Anchors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anchor = await _context.Anchor
                .Include(a => a.Asset)
                .Include(a => a.Scene)
                .FirstOrDefaultAsync(m => m.AnchorId == id);
            if (anchor == null)
            {
                return NotFound();
            }

            return View(anchor);
        }

        // GET: Anchors/Create
        public IActionResult Create()
        {
            ViewData["AssetId"] = new SelectList(_context.SceneAsset, "AssetId", "AssetId");
            ViewData["SceneId"] = new SelectList(_context.Scene, "SceneId", "SceneId");
            return View();
        }

        // POST: Anchors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnchorId,SceneId,AssetId,Scale")] Anchor anchor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anchor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.SceneAsset, "AssetId", "AssetId", anchor.AssetId);
            ViewData["SceneId"] = new SelectList(_context.Scene, "SceneId", "SceneId", anchor.SceneId);
            return View(anchor);
        }

        // GET: Anchors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anchor = await _context.Anchor.FindAsync(id);
            if (anchor == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.SceneAsset, "AssetId", "AssetId", anchor.AssetId);
            ViewData["SceneId"] = new SelectList(_context.Scene, "SceneId", "SceneId", anchor.SceneId);
            return View(anchor);
        }

        // POST: Anchors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnchorId,SceneId,AssetId,Scale")] Anchor anchor)
        {
            if (id != anchor.AnchorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anchor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnchorExists(anchor.AnchorId))
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
            ViewData["AssetId"] = new SelectList(_context.SceneAsset, "AssetId", "AssetId", anchor.AssetId);
            ViewData["SceneId"] = new SelectList(_context.Scene, "SceneId", "SceneId", anchor.SceneId);
            return View(anchor);
        }

        // GET: Anchors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anchor = await _context.Anchor
                .Include(a => a.Asset)
                .Include(a => a.Scene)
                .FirstOrDefaultAsync(m => m.AnchorId == id);
            if (anchor == null)
            {
                return NotFound();
            }

            return View(anchor);
        }

        // POST: Anchors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anchor = await _context.Anchor.FindAsync(id);
            _context.Anchor.Remove(anchor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnchorExists(int id)
        {
            return _context.Anchor.Any(e => e.AnchorId == id);
        }
    }
}
