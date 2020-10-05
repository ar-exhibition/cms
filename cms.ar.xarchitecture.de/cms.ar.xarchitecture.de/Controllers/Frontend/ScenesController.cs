﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms.ar.xarchitecture.de.cmsXARCH;
using Microsoft.AspNetCore.Http;
using Net.Codecrete.QrCodeGenerator;
using Vlingo.UUID;
using System.Drawing.Imaging;
using Org.BouncyCastle.Math.EC;
using System.IO;

namespace cms.ar.xarchitecture.de.Controllers.Frontend
{
    public class ScenesController : Controller
    {
        private readonly cmsXARCHContext _context;
        private NameBasedGenerator uuidCreator;

        public ScenesController(cmsXARCHContext context)
        {
            _context = context;
            uuidCreator = new NameBasedGenerator(HashType.SHA1);
        }

        // GET: Scenes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Scene.ToListAsync());
        }

        // GET: Scenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scene
                .FirstOrDefaultAsync(m => m.SceneId == id);
            if (scene == null)
            {
                return NotFound();
            }

            return View(scene);
        }

        // GET: Scenes/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Scenes/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("SceneId,SceneName,FileUuid,MarkerUuid")] Scene scene)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(scene);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(scene);
        //}

        // POST: Scenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SceneSubmissionValues values)
        {
            Scene newRecord = new Scene();

            newRecord.SceneName = values.SceneName;
            newRecord.FileUuid = null; //will be generated by client app

            if (values.FileToUpload == null || values.FileToUpload.Length == 0)
            {
                newRecord.MarkerUuid = createQRCode(values.SceneName); //give back UUID
            }
            else
            {
                newRecord.MarkerUuid = uploadToFilesystem(values.FileToUpload); //give back UUID
            }

            if (ModelState.IsValid)
            {
                _context.Add(newRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction("About", "Home"); //prb put some nice "you're done" view here!
            }
            return RedirectToAction("About", "Home"); //Error page?
        }

        // GET: Scenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scene.FindAsync(id);
            if (scene == null)
            {
                return NotFound();
            }
            return View(scene);
        }

        // POST: Scenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SceneId,SceneName,FileUuid,MarkerUuid")] Scene scene)
        {
            if (id != scene.SceneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SceneExists(scene.SceneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(scene);
        }

        // GET: Scenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scene
                .FirstOrDefaultAsync(m => m.SceneId == id);
            if (scene == null)
            {
                return NotFound();
            }

            return View(scene);
        }

        // POST: Scenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scene = await _context.Scene.FindAsync(id);
            _context.Scene.Remove(scene);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SceneExists(int id)
        {
            return _context.Scene.Any(e => e.SceneId == id);
        }

        private string uploadToFilesystem(IFormFile file)
        {
            return "";
        }

        private string createQRCode(string sceneName)
        {
            string markerUUID = uuidCreator.GenerateGuid(sceneName + DateTime.Now).ToString();
            QrCode qr = QrCode.EncodeText(markerUUID, QrCode.Ecc.Medium);

            string dir = Directory.GetCurrentDirectory();
            string filename = markerUUID + ".png";

            var path = Path.Combine(
                dir, "content", "marker",
                filename);

            using (var stream = new FileStream(path, FileMode.Create))
            using (var bitmap = qr.ToBitmap(40, 1))
            {
                bitmap.Save(stream, ImageFormat.Png);
            }

            return markerUUID;
        }
    }

    public class SceneSubmissionValues
    {
        public string SceneName { get; set; }
        public IFormFile FileToUpload { get; set; }
    }

}
