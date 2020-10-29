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
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System.Net;

namespace cms.ar.xarchitecture.de.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnchorsController : ControllerBase
    {
        private IMongoCollection<Anchor> _anchorsCollection;
        //private IMongoCollection<Asset> _assetsCollection;

        public AnchorsController(IMongoClient client)
        {
            var database = client.GetDatabase(Backend.DatabaseName);
            _anchorsCollection = database.GetCollection<Anchor>("Anchors");            
        }

        // GET: api/<AnchorsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<AnchorsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<AnchorsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] JsonElement body)
        {
            AnchorList anchorList = JsonSerializer.Deserialize<AnchorList>(body.GetRawText());
            List<JSONAnchor> jSONAnchors = anchorList.anchors.ToList();

            // define operator for this
            foreach (JSONAnchor anchor in jSONAnchors)
            {
                Anchor parsedAnchor;

                try
                {
                    parsedAnchor = _anchorsCollection.Find(a => a._id == ObjectId.Parse(anchor._id)).FirstOrDefault();
                }

                catch (Exception e)
                {
                    parsedAnchor = new Anchor();
                    parsedAnchor._id = new ObjectId();
                }

                parsedAnchor.AnchorID = anchor.AnchorID;
                parsedAnchor.AssetID = ObjectId.Parse(anchor.AssetID);
                parsedAnchor.SceneID = ObjectId.Parse(anchor.SceneID);
                parsedAnchor.Transform = anchor.Transform;
                parsedAnchor.Rotation = anchor.Rotation;
                parsedAnchor.Scale = anchor.Scale;

                if(parsedAnchor._id == default)
                {
                    await _anchorsCollection.InsertOneAsync(parsedAnchor);
                }
                else
                {
                    await _anchorsCollection.ReplaceOneAsync(a => a._id == parsedAnchor._id, parsedAnchor, new ReplaceOptions { IsUpsert = true });
                }
            }

            return CreatedAtAction("PostAnchors", jSONAnchors);
        }

        //// PUT api/<AnchorsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<AnchorsController>/5
        [HttpDelete("{pid}")]
        public IActionResult Delete(string pid)
        {
            ObjectId id = ObjectId.Parse(pid);

            _anchorsCollection.DeleteOneAsync(s => s._id == id);

            return Ok();
        }
    }
}
