using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cms.ar.xarchitecture.de.cmsDatabase;
using cms.ar.xarchitecture.de.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cms.ar.xarchitecture.de.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly cmsDatabaseContext _context;

        public ContentController(cmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Content
        [HttpGet]
        public async Task<String> Get()
        {
            Content content = new Content();
          
            return await content.getJson(_context);
        }

        // GET api/<APIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<APIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<APIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<APIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
