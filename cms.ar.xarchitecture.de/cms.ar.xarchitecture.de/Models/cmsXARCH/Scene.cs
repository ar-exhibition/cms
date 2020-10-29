using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Scene
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string SceneName { get; set; }
        public string WorldMapFileName { get; set; }
        public string WorldMapFileLink { get; set; }
        public string MarkerFileName { get; set; }
        public string MarkerFileLink { get; set; }
        public DateTime DateChanged { get; set; }
        public List<ObjectId> Assets { get; set; }
    }

    public class SceneView
    {
        public ObjectId _id { get; set; }
        public string SceneName { get; set; }
        public string MarkerFileName { get; set; }
        public string MarkerFileLink { get; set; }
        public DateTime DateChanged { get; set; }
    }
}
