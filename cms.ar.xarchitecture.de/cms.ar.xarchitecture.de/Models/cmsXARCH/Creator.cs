using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Creator
    {
        public ObjectId _id { get; set; }
        public string CreatorName { get; set; }
        public ObjectId[] Assets { get; set; }
    }
}
