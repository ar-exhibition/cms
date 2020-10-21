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
        private IMongoCollection<Anchor> _anchorsCollection;
        private IMongoCollection<Asset> _assetsCollection;

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
            AnchorList Anchors = JsonSerializer.Deserialize<AnchorList>(body.GetRawText());
            Anchor tmp;

            foreach (POSTAnchor element in Anchors.Anchors){

                bool newDocument = false;

                try
                {
                    //does this really throw an exception?!
                    tmp = _anchorsCollection.Find(a => a.AnchorID.Equals(element.AnchorID)).FirstOrDefault();
                }
                catch
                {
                    tmp = new Anchor();
                    newDocument = true;
                }

                Asset asset = _assetsCollection.Find(a => a.AssetID.Equals(element.AssetID)).FirstOrDefault();

                tmp.AnchorID = element.AnchorID;
                tmp.AssetID = element.AssetID;
                tmp.Scale = element.Scale;

                if (newDocument)
                    _anchorsCollection.InsertOne(tmp);
                else
                {
                    //work in progress...
                    var filter = Builders<Anchor>.Filter.Eq(a => a.AnchorID, tmp.AnchorID);
                    var update = Builders<Anchor>.Update.Set
                    _anchorsCollection.UpdateOne(filter, tmp);
                }


                // await _context.SaveChangesAsync(); //is this still necessary with MonogoDB?

                tmp = null;
            }

            return CreatedAtAction("PostAnchors", Anchors);
        }

        //// PUT api/<AnchorsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AnchorsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
