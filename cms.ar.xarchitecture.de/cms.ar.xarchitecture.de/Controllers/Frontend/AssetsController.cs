using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms.ar.xarchitecture.de.cmsXARCH;
using MongoDB.Driver;
using cms.ar.xarchitecture.de.Helper;
using MongoDB.Bson;

namespace cms.ar.xarchitecture.de.Controllers.Frontend
{
    public class AssetsController : Controller
    {
        private IMongoCollection<Asset> _assetsCollection;
        private IMongoCollection<Course> _courseCollection;
        private IMongoCollection<Creator> _creatorsCollection;

        public AssetsController(IMongoClient client)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _assetsCollection = database.GetCollection<Asset>("Assets");
            _courseCollection = database.GetCollection<Course>("Courses");
            _creatorsCollection = database.GetCollection<Creator>("Creators");
        }

        // GET: SceneAssets
        public async Task<IActionResult> Index()
        {
            //var cmsXARCHContext = _context.SceneAsset.Include(s => s.CourseNavigation).Include(s => s.CreatorNavigation).Include(s => s.ThumbnailUu);
            return View(await _assetsCollection.Find(a => true).ToListAsync());
        }

        // GET: SceneAssets/Details/5
        public async Task<IActionResult> Details(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sceneAsset = await _assetsCollection.FindAsync(a => a._id == id);

            if (sceneAsset == null)
            {
                return NotFound();
            }

            return View(sceneAsset);
        }

        // GET: SceneAssets/Create
        public IActionResult Create()
        {
            ViewData["Course"] = new SelectList(_courseCollection.AsQueryable());
            ViewData["Creator"] = new SelectList(_creatorsCollection.AsQueryable());
            return View();
        }

        // POST: SceneAssets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("AssetId,Creator,Course,AssetName,FileUuid,ExternalLink,ThumbnailUuid,CreationDate,Deleted")] SceneAsset sceneAsset)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(sceneAsset);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Course"] = new SelectList(_context.Course, "CourseId", "CourseId", sceneAsset.Course);
        //    ViewData["Creator"] = new SelectList(_context.Creator, "CreatorId", "CreatorId", sceneAsset.Creator);
        //    ViewData["ThumbnailUuid"] = new SelectList(_context.Thumbnail, "ThumbnailUuid", "ThumbnailUuid", sceneAsset.ThumbnailUuid);
        //    return View(sceneAsset);
        //}

        // GET: SceneAssets/Edit/5
        public async Task<IActionResult> Edit(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sceneAsset = await _assetsCollection.FindAsync(a => a._id == id);

            if (sceneAsset == null)
            {
                return NotFound();
            }
            ViewData["Course"] = new SelectList(_courseCollection.AsQueryable());
            ViewData["Creator"] = new SelectList(_creatorsCollection.AsQueryable());
            return View(sceneAsset);
        }

        // POST: SceneAssets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ObjectId id, [Bind("AssetId,Creator,Course,AssetName,FileUuid,ExternalLink,ThumbnailUuid,CreationDate,Deleted")] Asset asset)
        {
            if (id != asset._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _assetsCollection.UpdateOneAsync(r => r._id == id, asset.ToBsonDocument());
                }
                catch (MongoException e) 
                {
                    return NotFound(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Course"] = new SelectList(_courseCollection.AsQueryable());
            ViewData["Creator"] = new SelectList(_creatorsCollection.AsQueryable());
            return View(asset);
        }

        // GET: SceneAssets/Delete/5
        public async Task<IActionResult> Delete(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sceneAsset = await _assetsCollection.FindAsync(a => a._id == id);
                //.Include(s => s.CourseNavigation)
                //.Include(s => s.CreatorNavigation)
                //.Include(s => s.ThumbnailUu)
                //.FirstOrDefaultAsync(m => m.AssetId == id);
            if (sceneAsset == null)
            {
                return NotFound();
            }

            return View(sceneAsset);
        }

        // POST: SceneAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            var sceneAsset = await _assetsCollection.DeleteOneAsync(a => a._id == id);
            //_context.SceneAsset.Remove(sceneAsset);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool SceneAssetExists(ObjectId id)
        //{
        //    return _context.SceneAsset.Any(e => e.AssetId == id);
        //}
    }
}
