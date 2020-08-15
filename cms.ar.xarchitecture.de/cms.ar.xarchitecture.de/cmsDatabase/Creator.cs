using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class Creator
    {
        public Creator()
        {
            SceneAsset = new HashSet<SceneAsset>();
        }

        public int CreatorId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Studies { get; set; }

        public virtual ICollection<SceneAsset> SceneAsset { get; set; }
    }
}
