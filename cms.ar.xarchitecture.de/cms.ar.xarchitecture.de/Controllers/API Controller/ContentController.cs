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

            List<Asset> rawAssets = await _database.GetCollection<Asset>("Assets").Find(f => true).ToListAsync();
            IMongoCollection<Creator> creators = _database.GetCollection<Creator>("Creators");
            IMongoCollection<Course> courses = _database.GetCollection<Course>("Courses");

            List <AssetWrapper> assets = new List<AssetWrapper>();

            //create operator for this!
            foreach(Asset asset in rawAssets)
            {
                AssetWrapper wrappedAsset = new AssetWrapper
                {
                    _id = asset._id,
                    AssetName = asset.AssetName,
                    AssetType = asset.AssetType,
                    AssetFilename = asset.AssetFilename,
                    AssetLink = asset.AssetLink,
                    ExternalLink = asset.ExternalLink,
                    ThumbnailFilename = asset.ThumbnailFilename,
                    ThumbnailLink = asset.ThumbnailLink,
                    CreationDate = asset.CreationDate,
                    Deleted = asset.Deleted,
                    Creator = creators.AsQueryable().FirstOrDefault(c => c._id == asset.Creator),
                    Course = courses.AsQueryable().FirstOrDefault(co => co._id == asset.Course)
                };

                assets.Add(wrappedAsset);
            }

            Content content = new Content
            {
                Assets = assets,
                Anchors = await _database.GetCollection<Anchor>("Anchors").Find(f => true).ToListAsync(),
                Scenes = await _database.GetCollection<Scene>("Scenes").Find(f => true).ToListAsync()
            };

            //generate link on request time
            foreach (Asset asset in content.Assets)
            {
                asset.AssetLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.Asset, preamble, asset.AssetFilename);
                asset.ThumbnailLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.Thumbnail, preamble, asset.ThumbnailFilename);
            }

            foreach (Scene scene in content.Scenes)
            {
                scene.WorldMapFileLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.WorldMap, preamble, scene.WorldMapFileName);
                scene.MarkerFileLink = Backend.MapFilenameToDownloadLink(Backend.ContentType.Marker, preamble, scene.MarkerFileName);
            }

            return JsonConvert.SerializeObject(content, Formatting.Indented);
        }
    }
}
