using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Anchor
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string AnchorID { get; set; }
        public ObjectId AssetID { get; set; }
        public ObjectId SceneID { get; set; }
        public float[] Transform { get; set; }
        public float[] Rotation { get; set; }
        public float? Scale { get; set; }
    }

    //parsing post raw body to anchor list
    public class AnchorList
    {
        public JSONAnchor[] anchors { get; set; }
    }

    //this class is necessary since the usual json serializers don't support Bson.ObjectIds
    public class JSONAnchor
    {
        public string _id { get; set; }
        public string AnchorID { get; set; }
        public string AssetID { get; set; }
        public string SceneID { get; set; }
        public float[] Transform { get; set; }
        public float[] Rotation { get; set; }
        public float? Scale { get; set; }
    }
}
