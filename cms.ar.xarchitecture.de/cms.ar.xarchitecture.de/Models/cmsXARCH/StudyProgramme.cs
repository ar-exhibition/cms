using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using cms.ar.xarchitecture.de.Models.cmsXARCH;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class StudyProgramme
    {
        public ObjectId _id { get; set; }
        public string ProgrammeName { get; set; }
        public string University { get; set; }
        public ObjectId[] Courses { get; set; }

        [BsonIgnore]
        public virtual University UniversityNavigation { get; set; }

    }
}
