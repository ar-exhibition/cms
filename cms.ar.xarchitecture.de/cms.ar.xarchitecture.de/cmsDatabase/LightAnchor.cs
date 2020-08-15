using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class LightAnchor
    {
        public int AnchorId { get; set; }
        public int AssetId { get; set; }

        public virtual LightAsset Asset { get; set; }
        public virtual Anchor Anchor { get; set; }
    }
}
