using cms.ar.xarchitecture.de.cmsDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cms.ar.xarchitecture.de.Models
{
    public class Content
    {
        public IEnumerable<SceneAsset> assets;
        public IEnumerable<Anchor> anchors;
        public IEnumerable<Scene> scenes;
    }

}
