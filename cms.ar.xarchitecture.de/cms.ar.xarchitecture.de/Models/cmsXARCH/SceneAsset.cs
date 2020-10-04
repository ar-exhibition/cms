using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class SceneAsset
    {
        public SceneAsset()
        {
            Anchor = new HashSet<Anchor>();
        }

        public int AssetId { get; set; }
        public int? Creator { get; set; }
        public int? Course { get; set; }
        public string AssetName { get; set; }
        public string FileUuid { get; set; }
        public string ExternalLink { get; set; }
        public string ThumbnailUuid { get; set; }
        public DateTime? CreationDate { get; set; }
        public byte? Deleted { get; set; }

        public virtual Course CourseNavigation { get; set; }
        public virtual Creator CreatorNavigation { get; set; }
        public virtual Thumbnail ThumbnailUu { get; set; }
        public virtual ICollection<Anchor> Anchor { get; set; }
    }
}
