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
    public class TermsController : Controller
    {
        private IMongoCollection<Term> _terms;

        public TermsController(IMongoClient client)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _terms = database.GetCollection<Term>("Terms");
        }

        // GET: Terms
        public async Task<IActionResult> Index()
        {
            return View(await _terms.Find(t => true).ToListAsync());
        }

        // GET: Terms/Details/5
        public async Task<IActionResult> Details(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _terms.FindAsync(t => t._id == id);

            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // GET: Terms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Terms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id,TermName")] Term term)
        {
            if (ModelState.IsValid)
            {
                await _terms.InsertOneAsync(term);
                return RedirectToAction(nameof(Index));
            }
            return View(term);
        }

        // GET: Terms/Edit/5
        public async Task<IActionResult> Edit(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _terms.FindAsync(t => t._id == id);
            if (term == null)
            {
                return NotFound();
            }
            return View(term);
        }

        // POST: Terms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ObjectId id, [Bind("_id,TermName")] Term term)
        {
            if (id != term._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _terms.UpdateOneAsync(t => t._id == id, term.ToBsonDocument());
                }
                catch (MongoException e)
                {
                    return NotFound(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(term);
        }

        // GET: Terms/Delete/5
        public async Task<IActionResult> Delete(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _terms.FindAsync(t => t._id == id);

            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // POST: Terms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            await _terms.DeleteOneAsync(t => t._id == id);
            return RedirectToAction(nameof(Index));
        }

        //private bool TermExists(int id)
        //{
        //    return _context.Term.Any(e => e.TermId == id);
        //}
    }
}
