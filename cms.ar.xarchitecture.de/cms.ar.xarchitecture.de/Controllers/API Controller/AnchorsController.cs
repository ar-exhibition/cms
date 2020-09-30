using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cms.ar.xarchitecture.de.cmsDatabase;

//using Newtonsoft.Json;
using System.Text.Json;
using cms.ar.xarchitecture.de.Models.Wrapper;

namespace cms.ar.xarchitecture.de.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnchorsController : ControllerBase
    {
        private readonly cmsDatabaseContext _context;

        public AnchorsController(cmsDatabaseContext context)
        {
            _context = context;
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
        public async Task<ActionResult> PostAnchors([FromBody] JsonElement body)
        {
            AnchorList anchors = JsonSerializer.Deserialize<AnchorList>(body.GetRawText());

            foreach (Anchor element in anchors.anchors){
                _context.Anchor.Add(element);
                await _context.SaveChangesAsync();
            }

            //return CreatedAtAction("Get", new { id = anchors.AnchorId }, anchors);
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
