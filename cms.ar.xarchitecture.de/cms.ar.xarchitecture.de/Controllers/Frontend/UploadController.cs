using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Vlingo.UUID;
using cms.ar.xarchitecture.de.cmsXARCH;
using System.Collections;
using cms.ar.xarchitecture.de.Models.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace cms.ar.xarchitecture.de.Controllers
{
    public class UploadController : Controller
    {
        NameBasedGenerator uuidCreator;
        private readonly IFileProvider fileProvider;
        cmsXARCHContext _context;

        public UploadController(IFileProvider fileProvider, cmsXARCHContext context)
        {
            this.fileProvider = fileProvider;
            uuidCreator = new NameBasedGenerator(HashType.SHA1);
            _context = context;
        }

        // GET: Upload/Create
        public IActionResult Create()
        {
            ViewData["Programme"] = new SelectList(_context.Studies, "Programme", "Programme");

            List<String> cv = new List<String>();
            List<Course> cl = _context.Course.ToList();

            foreach (Course element in cl)
            {
                cv.Add(new String(element.Term + " " + element.Course1));
            }

            Console.WriteLine(cv.ToList());
            ViewData["CourseName"] = new SelectList(cv.ToList());

            return View();
        }

        [HttpPost]
        [RequestSizeLimit(62_914_560)]
        public async Task<IActionResult> SubmitFile(AssetSubmissionValues values)
        {
            Creator newCreator = new Creator();
            SceneAsset newAsset = new SceneAsset();
            List<Course> cl = _context.Course.ToList();

            if (values.FileToUpload == null || values.FileToUpload.Length == 0)
                return Content("file not selected");

            string filename = uuidCreator.GenerateGuid(values.FileToUpload.FileName + DateTime.Now) + ".glb";

            string dir = Directory.GetCurrentDirectory();

            var path = Path.Combine(
                        dir, "static", "content" ,"assets",
                        filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await values.FileToUpload.CopyToAsync(stream);
            }

            newCreator.Creator1 = values.creator;
            newCreator.Programme = values.programme;

            if (ModelState.IsValid)
            {
                _context.Creator.Add(newCreator);
                await _context.SaveChangesAsync();
            }

            newAsset.Creator = getCreatorID(newCreator.Creator1);
            newAsset.Course = getCourseID(values.programme, values.course);
            newAsset.AssetName = values.assetName;
            newAsset.FileUuid = filename; //with uuid
            newAsset.ExternalLink = null;
            newAsset.ThumbnailUuid = null;
            newAsset.CreationDate = DateTime.Now;
            newAsset.Deleted = 0;            

            if (ModelState.IsValid)
            {
                _context.SceneAsset.Add(newAsset);
                await _context.SaveChangesAsync();
                return RedirectToAction("About", "Home"); //prb put some nice "you're done" view here!
            }

            return RedirectToAction("About", "Home"); //prb to error or so...
        }

        private int? getCourseID(String programme, String course)
        {
            //dirty, huh?
            String[] arr = course.Split(" ");
            String term = arr[0];
            String courseName = "";

            for( int i = 1; i<arr.Length; i++)
            {
                courseName += " " + arr[i];
            }

            courseName = courseName.Trim();

            var courses = from m in _context.Course select m;
            courses = courses.Where(s => s.Course1.Contains(courseName));
            courses = courses.Where(s => s.Term.Contains(term));
            courses = courses.Where(s => s.Programme.Contains(programme));

            List<Course> result = courses.ToList();

            try
            {
                return result[0].CourseId;
            }
            catch
            {
                return 0;
            }
        }

        private int? getCreatorID(String creatorName)
        {
            var creators = from m in _context.Creator select m;
            creators = creators.Where(s => s.Creator1.Contains(creatorName));

            List<Creator> result = creators.ToList();

            try
            {
                return result[0].CreatorId;
            }
            catch
            {
                return 0;
            }
        }
    }
}
