using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Anchor
    {
        public string AnchorId { get; set; }
        public int AssetId { get; set; }
        public float? Scale { get; set; }

        public virtual SceneAsset Asset { get; set; }
    }
}
