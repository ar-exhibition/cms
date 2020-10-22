using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cms.ar.xarchitecture.de.cmsXARCH;
using cms.ar.xarchitecture.de.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using MongoDB.Driver;
using cms.ar.xarchitecture.de.Helper;
using MongoDB.Bson;

namespace cms.ar.xarchitecture.de.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private IMongoDatabase _database;
        private string host;
        private string prot;

        public ContentController(IMongoClient client, IHttpContextAccessor httpContextAccessor)
        {
            _database = client.GetDatabase(Backend.DatabaseName);

            host = httpContextAccessor.HttpContext.Request.Host.Value;
            prot = httpContextAccessor.HttpContext.Request.Scheme;
        }

        //GET: api/Content
        [HttpGet]
        public async Task<String> Get()
        {
            return await getContent();
        }

        //// GET api/<APIController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<APIController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<APIController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<APIController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{            
        //}

        public async Task<String> getContent()
        {
            string preamble = prot + "://" + host; //this data is only known on cotroller level. A little dirty, sry :-(

            Content content = new Content
            {
                Assets = await _database.GetCollection<Asset>("Assets").Find(f => true).ToListAsync(),
                Anchors = await _database.GetCollection<Anchor>("Anchors").Find(f => true).ToListAsync(),
                Scenes = await _database.GetCollection<Scene>("Scenes").Find(f => true).ToListAsync()
            };

            //generate link on request time
            foreach (Asset asset in content.Assets)
            {
                asset.GLTFLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.Asset, preamble, asset.FileUUID);
                asset.USDZLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.Asset, preamble, asset.FileUUID);
                asset.ThumbnailLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.Thumbnail, preamble, asset.ThumbnailUUID);
            }

            foreach (Scene scene in content.Scenes)
            {
                scene.WorldMapFileLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.WorldMap, preamble, scene.WorldMapFileUUID);
                scene.MarkerFileLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.Marker, preamble, scene.MarkerFileUUID);
            }

            return JsonConvert.SerializeObject(content, Formatting.Indented);
        }
    }
}
