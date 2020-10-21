using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Anchor
    {
        public ObjectId AnchorID { get; set; }
        public string AnchorUUID { get; set; }
        public int AssetId { get; set; }
        public float[] Transoform { get; set; }
        public float[] Rotation { get; set; }
        public float? Scale { get; set; }
    }
}
