﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cms.ar.xarchitecture.de.Models.Wrapper
{
    public class AnchorList
    {
        public POSTAnchor[] Anchors { get; set; }
    }

    public class POSTAnchor
    {        
        public string AnchorUUID { get; set; }
        public string AssetID { get; set; }
        public float? Scale { get; set; }
    }
}