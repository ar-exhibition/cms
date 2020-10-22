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
            List<Anchor> Anchors = JsonSerializer.Deserialize<List<Anchor>>(body.GetRawText());
            Anchor tmp;

            foreach (Anchor anchor in Anchors){

                //work in progress...
                var filter = Builders<Anchor>.Filter.Eq(a => a.AnchorID, anchor.AnchorID);
                var update = Builders<Anchor>.Update.Set(s => s.Scale, anchor.Scale);

                var options = new UpdateOptions();
                options.IsUpsert = true;

                await _anchorsCollection.UpdateOneAsync(filter, update, options);
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
