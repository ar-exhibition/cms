using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Creator
    {
        public Creator()
        {
            Asset = new HashSet<Asset>();
        }

        public ObjectId CreatorId { get; set; }
        public string CreatorName { get; set; }

        public virtual ICollection<Asset> Asset { get; set; }
    }
}
