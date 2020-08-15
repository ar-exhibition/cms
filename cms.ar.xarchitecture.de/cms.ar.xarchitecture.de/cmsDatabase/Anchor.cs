using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class Anchor
    {
        public int AnchorId { get; set; }
        public int SceneId { get; set; }
        public string Translate { get; set; }
        public string Rotate { get; set; }
        public string Scale { get; set; }

        public virtual Scene Scene { get; set; }
        public virtual SceneAssetAnchor SceneAssetAnchor { get; set; }
        public virtual LightAnchor LightAnchor { get; set; }
        public virtual MarkerAnchor MarkerAnchor { get; set; }

    }
}
