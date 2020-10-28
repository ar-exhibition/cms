using cms.ar.xarchitecture.de.Models.cmsXARCH;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    [BsonDiscriminator("Course")]
    public partial class Course
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string CourseName { get; set; }
        public string StudyProgramme { get; set; }
        public string Term { get; set; }
        public string University { get; set; }
        public List<ObjectId> Assets { get; set; }

        [BsonIgnore]
        public virtual StudyProgramme ProgrammeNavigation { get; set; }
        [BsonIgnore]
        public virtual Term TermNavigation { get; set; }
        [BsonIgnore]
        public virtual University UniversityNavigation { get; set; }
    }
}
