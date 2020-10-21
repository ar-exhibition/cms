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

        public ObjectId AssetId { get; set; }
        public int? Creator { get; set; }
        public int? Course { get; set; }
        public string AssetName { get; set; }
        public string AssetType { get; set; }
        public string FileUUID { get; set; }
        public string ExternalLink { get; set; }
        public string ThumbnailUUID { get; set; }
        public DateTime? CreationDate { get; set; }
        public byte? Deleted { get; set; }

        public virtual ICollection<Anchor> Anchor { get; set; }
    }
}
