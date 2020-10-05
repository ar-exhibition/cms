using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cms.ar.xarchitecture.de.cmsXARCH;

namespace cms.ar.xarchitecture.de.Controllers.Frontend
{
    public class QRCodeController : Controller
    {
        private readonly cmsXARCHContext _context;

        public QRCodeController(cmsXARCHContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Open(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            var scenes = from m in _context.Scene select m;
            scenes = scenes.Where(s => s.MarkerUuid.Contains(uuid));
            List<Scene> result = scenes.ToList();

            string sceneName = result[0].SceneName;

            ViewData["uuid"] = uuid;
            ViewData["scene"] = sceneName;

            return View();
        }

        public async Task<IActionResult> getHome()
        {
            return RedirectToAction("About", "Home");
        }
    }
}
