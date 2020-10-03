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
using cms.ar.xarchitecture.de.cmsDatabase;

namespace cms.ar.xarchitecture.de.Controllers
{
    public class UploadController : Controller
    {
        NameBasedGenerator uuidCreator;
        private readonly IFileProvider fileProvider;
        cmsDatabaseContext _context;

        public UploadController(IFileProvider fileProvider, cmsDatabaseContext context)
        {
            this.fileProvider = fileProvider;
            uuidCreator = new NameBasedGenerator(HashType.SHA1);
            _context = context;
        }

        // GET: Upload/Create
        public IActionResult Create()
        {
            ViewData["CourseName"] = new SelectList(_context.Course, "CourseId", "CourseName");
            ViewData["Programme"] = new SelectList(_context.Studies, "Programme", "Programme");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            string filename = uuidCreator.GenerateGuid(file.FileName) + ".glb";

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "/content/assets/",
                        filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Files");
        }
    }
}
