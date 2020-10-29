using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Creator
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string CreatorName { get; set; }
        public List<ObjectId> Assets { get; set; }
    }
}
