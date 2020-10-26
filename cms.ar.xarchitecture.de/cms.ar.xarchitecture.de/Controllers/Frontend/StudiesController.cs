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
    public class StudiesController : Controller
    {
        IMongoCollection<StudyProgramme> _studies;

        public StudiesController(IMongoClient client)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _studies = database.GetCollection<StudyProgramme>("StudyProgrammes");
            
        }

        // GET: Studies
        public async Task<IActionResult> Index()
        {
            return View(await _studies.Find(s => true).ToListAsync());
        }

        // GET: Studies/Details/5
        public async Task<IActionResult> Details(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studies = await _studies.FindAsync(s => s._id == id);
            
            if (studies == null)
            {
                return NotFound();
            }

            return View(studies);
        }

        // GET: Studies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id,ProgrammeName,University")] StudyProgramme studies)
        {
            if (ModelState.IsValid)
            {
                await _studies.InsertOneAsync(studies);
                return RedirectToAction(nameof(Index));
            }
            return View(studies);
        }

        // GET: Studies/Edit/5
        public async Task<IActionResult> Edit(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studies = await _studies.FindAsync(s => s._id == id);
            if (studies == null)
            {
                return NotFound();
            }
            return View(studies);
        }

        // POST: Studies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ObjectId id, [Bind("_id,ProgrammeName,University")] StudyProgramme studies)
        {
            if (id != studies._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _studies.UpdateOneAsync(s => s._id == id, studies.ToBsonDocument());
                    
                }
                catch (MongoException e)
                {
                    return NotFound(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studies);
        }

        // GET: Studies/Delete/5
        public async Task<IActionResult> Delete(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studies = await _studies.FindAsync(s => s._id == id);

            if (studies == null)
            {
                return NotFound();
            }

            return View(studies);
        }

        // POST: Studies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            var studies = await _studies.DeleteOneAsync(s => s._id == id);

            return RedirectToAction(nameof(Index));
        }

        //private bool StudiesExists(int id)
        //{
        //    return _context.Studies.Any(e => e.ProgrammeId == id);
        //}
    }
}
