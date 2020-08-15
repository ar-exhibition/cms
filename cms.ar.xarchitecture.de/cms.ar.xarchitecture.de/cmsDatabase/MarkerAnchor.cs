using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class MarkerAnchor
    {
        public int AnchorId { get; set; }
        public int AssetId { get; set; }

        public virtual MarkerAsset Asset { get; set; }
        public virtual Anchor Anchor { get; set; }
    }
}
