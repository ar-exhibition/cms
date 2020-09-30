using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cms.ar.xarchitecture.de.Models.Wrapper
{
    public class AnchorList
    {
        public POSTAnchor[] anchors { get; set; }
    }

    public class POSTAnchor
    {
        public string anchorId { get; set; }
        public int assetId { get; set; }
        public float? scale { get; set; }
    }
}
