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
        public async Task<IActionResult> SubmitFile(AssetSubmissionValues values)
        {
            if (values.FileToUpload == null || values.FileToUpload.Length == 0)
                return Content("file not selected");

            string filename = uuidCreator.GenerateGuid(values.FileToUpload.FileName) + ".glb";

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "/content/assets/",
                        filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await values.FileToUpload.CopyToAsync(stream);
            }

            return RedirectToAction("Files");
        }
    }
}
