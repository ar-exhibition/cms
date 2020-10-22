using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Asset
    {
        public Asset()
        {
            Anchor = new HashSet<Anchor>();
        }

        public ObjectId AssetID { get; set; }
        public string AssetName { get; set; }
        public string AssetType { get; set; }
        public string AssetFilename { get; set; }
        public string AssetLink { get; set; }
        public string ExternalLink { get; set; }
        public string ThumbnailFilename { get; set; }
        public string ThumbnailLink { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool Deleted { get; set; }
        public Creator Creator { get; set; }
        public Course Course { get; set; }

        public virtual ICollection<Anchor> Anchor { get; set; }
    }
}
