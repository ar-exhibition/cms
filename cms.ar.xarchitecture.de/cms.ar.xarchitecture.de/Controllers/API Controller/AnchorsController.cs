using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms.ar.xarchitecture.de.cmsXARCH;
using cms.ar.xarchitecture.de.Models.Wrapper;

//using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MongoDB.Driver;
using cms.ar.xarchitecture.de.Helper;

namespace cms.ar.xarchitecture.de.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnchorsController : ControllerBase
    {
        private readonly cmsXARCHContext _context;
        private IMongoCollection<Anchor> _anchorsCollection;

        public AnchorsController(IMongoClient client)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _anchorsCollection = database.GetCollection<Anchor>("Anchors");            
        }

        // GET: api/<AnchorsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AnchorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AnchorsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] JsonElement body)
        {
            AnchorList anchors = JsonSerializer.Deserialize<AnchorList>(body.GetRawText());
            Anchor tmp;

            foreach (POSTAnchor element in anchors.anchors){

                bool newRecord = false;
                tmp = _context.Anchor.Find(element.anchorId);

                if(tmp == null)
                {
                    tmp = new Anchor();
                    newRecord = true;
                }

                SceneAsset asset = _context.SceneAsset.Find(element.assetId);

                tmp.AnchorId = element.anchorId;
                tmp.AssetId = element.assetId;
                tmp.Scale = element.scale;
                tmp.Asset = asset;

                if (newRecord)
                    _context.Anchor.Add(tmp);
                else
                    _context.Anchor.Update(tmp);

                await _context.SaveChangesAsync();

                tmp = null;
            }

            return CreatedAtAction("PostAnchors", anchors);
        }

        // PUT api/<AnchorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AnchorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
