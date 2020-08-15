using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class SceneAsset
    {
        public SceneAsset()
        {
            SceneAssetAnchor = new HashSet<SceneAssetAnchor>();
        }

        public int AssetId { get; set; }
        public int Creator { get; set; }
        public int Course { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
        public DateTime? Date { get; set; }
        public string Link { get; set; }
        public string Thumbnail { get; set; }
        public byte? Deleted { get; set; }

        public virtual Course CourseNavigation { get; set; }
        public virtual Creator CreatorNavigation { get; set; }
        public virtual ICollection<SceneAssetAnchor> SceneAssetAnchor { get; set; }
    }
}
