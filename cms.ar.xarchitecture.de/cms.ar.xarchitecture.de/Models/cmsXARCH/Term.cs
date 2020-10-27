using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Term
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string TermName { get; set; }
        public ObjectId[] Courses { get; set; }

    }
}
