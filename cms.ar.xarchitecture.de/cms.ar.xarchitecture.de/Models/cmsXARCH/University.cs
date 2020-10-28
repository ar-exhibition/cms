using cms.ar.xarchitecture.de.cmsXARCH;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cms.ar.xarchitecture.de.Models.cmsXARCH
{
    public class University
    {
        [BsonId]
        public ObjectId _id { get; set; } 
        public string UniversityName { get; set; }
        public List<ObjectId> StudyProgrammes { get; set; }
    }
}
