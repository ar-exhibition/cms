using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms.ar.xarchitecture.de.cmsXARCH;
using MongoDB.Driver;
using MongoDB.Bson;
using cms.ar.xarchitecture.de.Models.cmsXARCH;
using cms.ar.xarchitecture.de.Helper;

namespace cms.ar.xarchitecture.de.Controllers.Frontend
{
    public class CoursesController : Controller
    {
        IMongoCollection<Course> _courses;
        IMongoCollection<StudyProgramme> _studies;
        IMongoCollection<Term> _terms;
        IMongoCollection<University> _unis;
        IMongoCollection<Asset> _assets;

        public CoursesController(IMongoClient client)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _courses = database.GetCollection<Course>("Courses");
            // add collections here!
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            //var cmsXARCHContext = _context.Course.Include(c => c.ProgrammeNavigation).Include(c => c.TermNavigation);
            return View(await _courses.Find(a => true).ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courses.FindAsync(c => c._id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["StudyProgramme"] = new SelectList(_studies.AsQueryable());
            ViewData["Term"] = new SelectList(_terms.AsQueryable());
            ViewData["University"] = new SelectList(_unis.AsQueryable());
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id,CourseName,StudyProgramme,Term,University")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _courses.InsertOneAsync(course);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudyProgramme"] = new SelectList(_studies.AsQueryable());
            ViewData["Term"] = new SelectList(_terms.AsQueryable());
            ViewData["University"] = new SelectList(_unis.AsQueryable());
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courses.FindAsync(c => c._id == id);

            if (course == null)
            {
                return NotFound();
            }
            ViewData["StudyProgramme"] = new SelectList(_studies.AsQueryable());
            ViewData["Term"] = new SelectList(_terms.AsQueryable());
            ViewData["University"] = new SelectList(_unis.AsQueryable());
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ObjectId id, [Bind("CourseId,Programme,Course1,Term")] Course course)
        {
            if (id != course._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courses.UpdateOneAsync(c => c._id == id, course.ToBsonDocument());
                }
                catch (MongoException e)
                {
                    return NotFound(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudyProgramme"] = new SelectList(_studies.AsQueryable());
            ViewData["Term"] = new SelectList(_terms.AsQueryable());
            ViewData["University"] = new SelectList(_unis.AsQueryable());
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courses.FindAsync(a => a._id == id);
            //.Include(c => c.ProgrammeNavigation)
            //.Include(c => c.TermNavigation)
            //.FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            var course = await _courses.DeleteOneAsync(c => c._id == id);
            //_context.Course.Remove(course);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool CourseExists(ObjectId id)
        //{
        //    return _courses.Find(c => true);
        //}
    }
}
