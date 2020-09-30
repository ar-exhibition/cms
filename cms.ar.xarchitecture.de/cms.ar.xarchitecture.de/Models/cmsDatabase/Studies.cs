using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class Studies
    {
        public Studies()
        {
            Course = new HashSet<Course>();
            Creator = new HashSet<Creator>();
        }

        public int Id { get; set; }
        public string Programme { get; set; }

        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Creator> Creator { get; set; }
    }
}
