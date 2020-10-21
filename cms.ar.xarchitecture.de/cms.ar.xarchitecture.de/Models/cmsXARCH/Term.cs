using MongoDB.Bson;
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

        public ObjectId TermId { get; set; }
        public string TermName { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }
}
