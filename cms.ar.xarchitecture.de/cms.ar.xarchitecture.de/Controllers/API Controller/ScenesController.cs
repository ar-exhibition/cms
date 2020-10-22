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
using Org.BouncyCastle.Asn1.X509;
using MongoDB.Driver;
using MongoDB.Bson;

namespace cms.ar.xarchitecture.de.Controllers.API_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenesController : ControllerBase
    {

        private IMongoCollection<Scene> _scenesCollection;
        private IHttpContextAccessor _host;

        public ScenesController (IMongoClient client, IHttpContextAccessor httpContextAccessor)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _scenesCollection = database.GetCollection<Scene>("Scenes");
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
            Scene document = new Scene{
                SceneID = ObjectId.Parse(data["SceneID"]),
                SceneName = data["SceneName"],
                WorldMapFileUUID = data["WorldMapFileUUID"],
                MarkerFileUUID = data["MarkerFileUUID"],
                DateChanged = DateTime.Now
            };

            if (document.SceneID.Equals(null))
                document.MarkerFileUUID = MarkerCreator.createQRCode(document.SceneName, _host.HttpContext.Request.Host.Value);

            Backend.SaveToFilesystem(data.Files.FirstOrDefault(), Backend.ContentType.WorldMap);

            var filter = Builders<Scene>.Filter.Eq(s => s.SceneID, document.SceneID);

            var update = Builders<Scene>.Update.Set(s => s.SceneName, document.SceneName);
            update = Builders<Scene>.Update.Set(s => s.WorldMapFileUUID, document.WorldMapFileUUID);
            update = Builders<Scene>.Update.Set(s => s.MarkerFileUUID, document.MarkerFileUUID);
            update = Builders<Scene>.Update.Set(s => s.DateChanged, document.DateChanged);

            var options = new UpdateOptions();
            options.IsUpsert = true;

            await _scenesCollection.UpdateOneAsync(filter, update, options);

            return JsonConvert.SerializeObject(document, Formatting.Indented);
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
