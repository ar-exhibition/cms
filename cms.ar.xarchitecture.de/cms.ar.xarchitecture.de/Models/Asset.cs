using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cms.ar.xarchitecture.de.Models
{
    public class Asset
    {
        public int id { get; set; }
        public int CreatorID { get; set; }
        public int CourseID { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
        public string Link { get; set; }
    }
}
