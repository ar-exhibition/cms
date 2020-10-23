using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Course
    {
        public ObjectId _id { get; set; }
        public string CourseName { get; set; }
        public string StudyProgramme { get; set; }
        public string Term { get; set; }
        public string University { get; set; }
        public ObjectId[] Assets { get; set; }
    }
}
