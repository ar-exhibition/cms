using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Scene
    {
        public ObjectId SceneId { get; set; }
        public string SceneName { get; set; }
        public string WorldMapUUID { get; set; }
        public string MarkerUUID { get; set; }
        public DateTime DateChanged { get; set; }
    }
}
