using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class Anchor
    {
        public int AnchorId { get; set; }
        public int SceneId { get; set; }
        public int? AssetId { get; set; }
        public string Scale { get; set; }

        public virtual SceneAsset Asset { get; set; }
        public virtual Scene Scene { get; set; }
    }
}
