using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Creator
    {
        public Creator()
        {
            SceneAsset = new HashSet<SceneAsset>();
        }

        public int CreatorId { get; set; }
        public string Creator1 { get; set; }
        public string Programme { get; set; }

        public virtual Studies ProgrammeNavigation { get; set; }
        public virtual ICollection<SceneAsset> SceneAsset { get; set; }
    }
}
