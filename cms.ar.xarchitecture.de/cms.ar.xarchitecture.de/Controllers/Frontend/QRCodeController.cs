using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cms.ar.xarchitecture.de.cmsXARCH;
using MongoDB.Driver;
using cms.ar.xarchitecture.de.Helper;
using Microsoft.EntityFrameworkCore;

namespace cms.ar.xarchitecture.de.Controllers.Frontend
{
    public class QRCodeController : Controller
    {
        IMongoCollection<Scene> _scenes;

        public QRCodeController(IMongoClient client)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _scenes = database.GetCollection<Scene>("Scenes");

        }

        public async Task<IActionResult> Open(string uuid)
        {
            if (uuid == null)
            {
                return NotFound();
            }

            Scene scene = _scenes.AsQueryable().FirstOrDefault(s => s.MarkerFileName.Contains(uuid));

            if(scene == default)
            {
                return NotFound();
            }

            ViewData["uuid"] = uuid;
            ViewData["scene"] = scene.SceneName;

            return View();
        }

        public IActionResult getHome()
        {
            return RedirectToAction("About", "Home");
        }
    }
}
