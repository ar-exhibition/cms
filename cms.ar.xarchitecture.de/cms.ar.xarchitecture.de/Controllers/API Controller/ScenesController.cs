using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cms.ar.xarchitecture.de.Models.Wrapper;
using cms.ar.xarchitecture.de.cmsXARCH;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using cms.ar.xarchitecture.de.Helper;

namespace cms.ar.xarchitecture.de.Controllers.API_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenesController : ControllerBase
    {

        cmsXARCHContext _context;
        private IHttpContextAccessor _host;

        public ScenesController (cmsXARCHContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _host = httpContextAccessor;
        }

        // GET api/<ScenesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "hello there...";
        //}

        // POST api/<ScenesController>
        [HttpPost]
        public async Task<String> Post(IFormCollection data)
        {

            string id = data["SceneID"];
            string sceneName = data["SceneName"];
            string fileUUID = data["FileUUID"];
            string markerUUID = data["MarkerUUID"];

            bool NoRecordExists = string.IsNullOrEmpty(id);
            Scene Record;
 
            if (NoRecordExists)
                Record = new Scene();

            else
                Record = _context.Scene.Find(Int32.Parse(id));


            if (sceneName != null)
                Record.SceneName = sceneName;

            if (fileUUID != null)
                Record.FileUuid = fileUUID;

            if (NoRecordExists)
                Record.MarkerUuid = MarkerCreator.createQRCode(Record.SceneName, _host.HttpContext.Request.Host.Value);
            else
                if (markerUUID != null)
                    Record.MarkerUuid = markerUUID;

            try
            {
                FormFile file = (FormFile)data.Files[0];
                string path = Path.Combine(Directory.GetCurrentDirectory(), "static", "content", "worldmaps", Record.FileUuid);

                using (var stream = new FileStream(path, FileMode.Create)) //create or overwrite
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch
            {
                Console.WriteLine("no file"); //put some meaningful http response here!
            }

            if (ModelState.IsValid)
            {
                if (NoRecordExists)
                    _context.Scene.Add(Record);
                else
                    _context.Scene.Update(Record);

                await _context.SaveChangesAsync();
            }
            return JsonConvert.SerializeObject(Record, Formatting.Indented);
        }

        //// PUT api/<ScenesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ScenesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    Console.WriteLine("Hello there..");
        //}
    }
}
