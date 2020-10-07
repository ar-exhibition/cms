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

namespace cms.ar.xarchitecture.de.Controllers.API_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenesController : ControllerBase
    {

        cmsXARCHContext _context;

        public ScenesController (cmsXARCHContext context)
        {
            _context = context;
        }

        // GET: api/<ScenesController>
        [HttpGet]
        public async Task<String> Get()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            SceneSubmissionValues ssv = new SceneSubmissionValues { name = "test", sceneFile = null };
            return JsonConvert.SerializeObject(ssv, Formatting.Indented);
            //return new string[] { "value1", "value2" };
        }

        // GET api/<ScenesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "";
        }

        // POST api/<ScenesController>
        [HttpPost]
        public void Post(IFormCollection data)
        {

            Scene newRecord = new Scene();
            newRecord.SceneName = data["SceneName"];
            newRecord.FileUuid = data["FileUUID"];

            try
            {
                FormFile file = (FormFile)data.Files[0];
                string path = Path.Combine(Directory.GetCurrentDirectory(), "content", "worldmaps", newRecord.FileUuid);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
            }
            catch
            {
                Console.WriteLine("no file"); //put some meaningful http response here!
            }

            if (ModelState.IsValid)
            {
                _context.Scene.Add(newRecord);
                _context.SaveChangesAsync();
            }
        }

        // PUT api/<ScenesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ScenesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
