using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class LightAsset
    {
        public LightAsset()
        {
            LightAnchor = new HashSet<LightAnchor>();
        }

        public int AssetId { get; set; }
        public string Type { get; set; }
        public int? Power { get; set; }
        public string Color { get; set; }

        public virtual ICollection<LightAnchor> LightAnchor { get; set; }
    }
}
