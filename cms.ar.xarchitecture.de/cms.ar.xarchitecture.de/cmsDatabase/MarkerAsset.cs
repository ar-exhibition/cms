using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class MarkerAsset
    {
        public MarkerAsset()
        {
            MarkerAnchor = new HashSet<MarkerAnchor>();
        }

        public int AssetId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public virtual ICollection<MarkerAnchor> MarkerAnchor { get; set; }
    }
}
