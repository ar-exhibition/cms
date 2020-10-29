using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace cms.ar.xarchitecture.de.Models.Wrapper
{
    public class SceneSubmissionValues
    {
        public string SceneName { get; set; }
        public IFormFile FileToUpload { get; set; }
    }
}
