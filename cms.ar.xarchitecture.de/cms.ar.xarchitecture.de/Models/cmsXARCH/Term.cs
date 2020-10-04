using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class Term
    {
        public Term()
        {
            Course = new HashSet<Course>();
        }

        public int TermId { get; set; }
        public string Term1 { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }
}
