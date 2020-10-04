using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms.ar.xarchitecture.de.cmsXARCH;

namespace cms.ar.xarchitecture.de.Controllers.Frontend
{
    public class SceneAssetsController : Controller
    {
        private readonly cmsXARCHContext _context;

        public SceneAssetsController(cmsXARCHContext context)
        {
            _context = context;
        }

        // GET: SceneAssets
        public async Task<IActionResult> Index()
        {
            var cmsXARCHContext = _context.SceneAsset.Include(s => s.CourseNavigation).Include(s => s.CreatorNavigation).Include(s => s.ThumbnailUu);
            return View(await cmsXARCHContext.ToListAsync());
        }

        // GET: SceneAssets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sceneAsset = await _context.SceneAsset
                .Include(s => s.CourseNavigation)
                .Include(s => s.CreatorNavigation)
                .Include(s => s.ThumbnailUu)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (sceneAsset == null)
            {
                return NotFound();
            }

            return View(sceneAsset);
        }

        // GET: SceneAssets/Create
        public IActionResult Create()
        {
            ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId");
            ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId");
            ViewData["ThumbnailUuid"] = new SelectList(_context.Thumbnail, "ThumbnailUuid", "ThumbnailUuid");
            return View();
        }

        // POST: SceneAssets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetId,Creator,Course,AssetName,FileUuid,ExternalLink,ThumbnailUuid,CreationDate,Deleted")] SceneAsset sceneAsset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sceneAsset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId", sceneAsset.Course);
            ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId", sceneAsset.Creator);
            ViewData["ThumbnailUuid"] = new SelectList(_context.Thumbnail, "ThumbnailUuid", "ThumbnailUuid", sceneAsset.ThumbnailUuid);
            return View(sceneAsset);
        }

        // GET: SceneAssets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sceneAsset = await _context.SceneAsset.FindAsync(id);
            if (sceneAsset == null)
            {
                return NotFound();
            }
            ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId", sceneAsset.Course);
            ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId", sceneAsset.Creator);
            ViewData["ThumbnailUuid"] = new SelectList(_context.Thumbnail, "ThumbnailUuid", "ThumbnailUuid", sceneAsset.ThumbnailUuid);
            return View(sceneAsset);
        }

        // POST: SceneAssets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetId,Creator,Course,AssetName,FileUuid,ExternalLink,ThumbnailUuid,CreationDate,Deleted")] SceneAsset sceneAsset)
        {
            if (id != sceneAsset.AssetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sceneAsset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SceneAssetExists(sceneAsset.AssetId))
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
            ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId", sceneAsset.Course);
            ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId", sceneAsset.Creator);
            ViewData["ThumbnailUuid"] = new SelectList(_context.Thumbnail, "ThumbnailUuid", "ThumbnailUuid", sceneAsset.ThumbnailUuid);
            return View(sceneAsset);
        }

        // GET: SceneAssets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sceneAsset = await _context.SceneAsset
                .Include(s => s.CourseNavigation)
                .Include(s => s.CreatorNavigation)
                .Include(s => s.ThumbnailUu)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (sceneAsset == null)
            {
                return NotFound();
            }

            return View(sceneAsset);
        }

        // POST: SceneAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sceneAsset = await _context.SceneAsset.FindAsync(id);
            _context.SceneAsset.Remove(sceneAsset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SceneAssetExists(int id)
        {
            return _context.SceneAsset.Any(e => e.AssetId == id);
        }
    }
}
