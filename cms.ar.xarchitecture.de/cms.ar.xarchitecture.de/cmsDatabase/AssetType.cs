using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class AssetType
    {
        public AssetType()
        {
            SceneAsset = new HashSet<SceneAsset>();
        }

        public int AssetTypeId { get; set; }
        public string Designator { get; set; }

        public virtual ICollection<SceneAsset> SceneAsset { get; set; }
    }
}
