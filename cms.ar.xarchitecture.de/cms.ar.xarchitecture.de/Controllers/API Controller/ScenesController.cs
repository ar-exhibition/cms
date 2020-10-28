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
using System.Web.WebPages;

namespace cms.ar.xarchitecture.de.Controllers.API_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenesController : ControllerBase
    {

        private IMongoCollection<Scene> _scenesCollection;
        private IMongoCollection<Asset> _assets;
        private IHttpContextAccessor _host;

        public ScenesController (IMongoClient client, IHttpContextAccessor httpContextAccessor)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _scenesCollection = database.GetCollection<Scene>("Scenes");
            _assets = database.GetCollection<Asset>("Assets");
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
            Scene scene;

            try
            {
                scene = _scenesCollection.Find(a => a._id == ObjectId.Parse(data["_id"])).FirstOrDefault();


                if (!data["SceneName"].ToString().IsEmpty())
                    scene.SceneName = data["SceneName"].ToString();

                if (!data["WorldMapFileName"].ToString().IsEmpty())
                    scene.WorldMapFileName = data["WorldMapFileName"].ToString();

            }
            catch (Exception e)
            {
                scene = new Scene();
                scene._id = new ObjectId();
                scene.SceneName = data["SceneName"];
                scene.WorldMapFileName = data["WorldMapFileName"];
                scene.MarkerFileName = MarkerCreator.createQRCode(scene.SceneName, _host.HttpContext.Request.Host.Value);
                scene.WorldMapFileLink = default;
                scene.MarkerFileLink = default;
                scene.DateChanged = DateTime.Now;
                scene.Assets = default;
            }

            if(data.Files.Count > 0)
            {
                await Backend.SaveToFilesystem(data.Files.FirstOrDefault(), Backend.ContentType.WorldMap);
            }

            if (scene._id == default)
                await _scenesCollection.InsertOneAsync(scene);
            else
                await _scenesCollection.ReplaceOneAsync(s => s._id == scene._id, scene, new ReplaceOptions { IsUpsert = true });

            return JsonConvert.SerializeObject(scene, Formatting.Indented);
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
