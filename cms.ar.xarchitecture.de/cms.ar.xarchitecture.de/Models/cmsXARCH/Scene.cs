using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Scene
    {
        public ObjectId SceneID { get; set; }
        public string SceneName { get; set; }
        public string WorldMapFileUUID { get; set; }
        public string MarkerFileUUID { get; set; }
        public DateTime DateChanged { get; set; }
    }
}
