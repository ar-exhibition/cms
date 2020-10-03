using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Thumbnail
    {
        public int ThumbnailId { get; set; }
        public string ThumbnailUuid { get; set; }

        public virtual SceneAsset SceneAsset { get; set; }
    }
}
