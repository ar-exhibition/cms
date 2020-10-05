using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cms.ar.xarchitecture.de.Models.Wrapper
{
    public class SceneSubmissionValues
    {
        public string name {get; set;}
        public IFormFile sceneFile { get; set; } 
    }
}
