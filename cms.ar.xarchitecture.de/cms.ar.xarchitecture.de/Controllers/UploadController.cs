using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace cms.ar.xarchitecture.de.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Upload()
        {
            return View();
        }
    }
}
