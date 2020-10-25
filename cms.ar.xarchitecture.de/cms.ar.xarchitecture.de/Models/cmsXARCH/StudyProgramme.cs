using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class StudyProgramme
    {
        public ObjectId _id { get; set; }
        public string ProgrammeName { get; set; }
        public string University { get; set; }
        public ObjectId[] Courses { get; set; }

    }
}
