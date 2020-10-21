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
        public string Creator { get; set; }
        public string StudyProgramme { get; set; }
        public string Course { get; set; }
        public string University { get; set; }
        public string AssetName { get; set; }
        public string TumbnailBase64 { get; set; }
        public IFormFile AssetFile { get; set; }
    }
}
