using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;


namespace cms.ar.xarchitecture.de.Controllers
{
    public class DownloadController : Controller
    {

        private readonly IFileProvider fileProvider;

        public DownloadController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public async Task<IActionResult> Get(string type, string file)
        {
            if (file == null)
                return Content("filename not present");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "static", "content", type, file);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetMimeMapping(file), Path.GetFileName(path));
        }

        public string GetMimeMapping(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            //add custom mime-mappings
            provider.Mappings.Add(".gltf", "model/gltf+json");

            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

    }
}
