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
    public class UploadController : Controller
    {
        private readonly cmsDatabaseContext _context;

        public UploadController(cmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Upload
        public async Task<IActionResult> Index()
        {
            var cmsDatabaseContext = _context.SceneAsset.Include(s => s.AssetTypeNavigation).Include(s => s.CourseNavigation).Include(s => s.CreatorNavigation);
            return View(await cmsDatabaseContext.ToListAsync());
        }

        // GET: Upload/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sceneAsset = await _context.SceneAsset
                .Include(s => s.AssetTypeNavigation)
                .Include(s => s.CourseNavigation)
                .Include(s => s.CreatorNavigation)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (sceneAsset == null)
            {
                return NotFound();
            }

            return View(sceneAsset);
        }

        // GET: Upload/Create
        public IActionResult Create()
        {
            ViewData["AssetType"] = new SelectList(_context.AssetType, "AssetTypeId", "AssetTypeId");
            ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId");
            ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId");
            return View();
        }

        // POST: Upload/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("AssetId,Creator,Course,Name,Filename,Filetype,Date,Link,Thumbnail,Type,Power,Color,Deleted,AssetType")] SceneAsset sceneAsset)
        public async Task<IActionResult> Create([Bind("CreatorID,Name,Studies,Course")] Creator creator, [Bind("AssetId,Creator,Course,Name,Filename,Filetype,Date,Link,Thumbnail,Type,Power,Color,Deleted,AssetType")] SceneAsset sceneAsset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creator); //new
                _context.Add(sceneAsset);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetType"] = new SelectList(_context.AssetType, "AssetTypeId", "AssetTypeId", sceneAsset.AssetType);
            ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId", sceneAsset.Course);
            ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId", sceneAsset.Creator);
            return View(sceneAsset);
        }

        // GET: Upload/Edit/5
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
            ViewData["AssetType"] = new SelectList(_context.AssetType, "AssetTypeId", "AssetTypeId", sceneAsset.AssetType);
            ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId", sceneAsset.Course);
            ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId", sceneAsset.Creator);
            return View(sceneAsset);
        }

        // POST: Upload/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetId,Creator,Course,Name,Filename,Filetype,Date,Link,Thumbnail,Type,Power,Color,Deleted,AssetType")] SceneAsset sceneAsset)
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
            ViewData["AssetType"] = new SelectList(_context.AssetType, "AssetTypeId", "AssetTypeId", sceneAsset.AssetType);
            ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId", sceneAsset.Course);
            ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId", sceneAsset.Creator);
            return View(sceneAsset);
        }

        // GET: Upload/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sceneAsset = await _context.SceneAsset
                .Include(s => s.AssetTypeNavigation)
                .Include(s => s.CourseNavigation)
                .Include(s => s.CreatorNavigation)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (sceneAsset == null)
            {
                return NotFound();
            }

            return View(sceneAsset);
        }

        // POST: Upload/Delete/5
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
