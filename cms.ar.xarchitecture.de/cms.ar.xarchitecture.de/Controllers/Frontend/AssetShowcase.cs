using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace cms.ar.xarchitecture.de.Controllers.Frontend
{
    public class AssetShowcaseController : Controller
    {
        IWebHostEnvironment _hostingEnvironment;

        public AssetShowcaseController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: AssetShowcaseController
        public IActionResult Index()
        {
            return View();
        }
    }
}