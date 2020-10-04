using cms.ar.xarchitecture.de.cmsXARCH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cms.ar.xarchitecture.de.Models.Wrapper
{
    public class AssetWrapper
    {
        public SceneAsset sceneAsset;
        public Creator creator;

        public virtual Course CourseNameNavigation { get; set; }
        public virtual Studies ProgrammeNavigation { get; set; }

    }
}
