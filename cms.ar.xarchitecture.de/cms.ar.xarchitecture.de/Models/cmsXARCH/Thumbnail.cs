using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Thumbnail
    {
        public ObjectId ThumbnailId { get; set; }
        public string ThumbnailUuid { get; set; }

        public virtual Asset SceneAsset { get; set; }
    }
}
