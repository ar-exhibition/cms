using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace cms.ar.xarchitecture.de.Models.Wrapper
{
    public class AssetSubmissionValues
    {
        public string creator { get; set; }
        public string programme { get; set; }
        public string course { get; set; }
        public string assetName { get; set; }
        public IFormFile FileToUpload { get; set; }
    }
}
