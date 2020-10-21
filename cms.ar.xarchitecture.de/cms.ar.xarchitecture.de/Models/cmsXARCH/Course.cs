using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Course
    {
        public Course()
        {
            Asset = new HashSet<Asset>();
        }
        public ObjectId CourseId { get; set; }
        public string StudyProgramme { get; set; }
        public string CourseName { get; set; }
        public string Term { get; set; }
        public string University { get; set; }

        public virtual ICollection<Asset> Asset { get; set; }
    }
}
