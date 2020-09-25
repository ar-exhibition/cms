using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class Scene
    {
        public Scene()
        {
            Anchor = new HashSet<Anchor>();
        }

        public int SceneId { get; set; }
        public string Name { get; set; }
        public string SceneFile { get; set; }

        public virtual ICollection<Anchor> Anchor { get; set; }
    }
}
