using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace cms.ar.xarchitecture.de.Controllers.Frontend
{
    public class ModelViewerController : Controller
    {
        IWebHostEnvironment _hostingEnvironment;

        public ModelViewerController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: ModelViewerController
        public ActionResult Index()
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "html", "ModelViewer", "Index.html");
            return PhysicalFile(path, "text/html");
        }
    }
}
