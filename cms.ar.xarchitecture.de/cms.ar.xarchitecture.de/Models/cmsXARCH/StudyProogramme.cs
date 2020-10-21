using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class StudyProogramme
    {
        public StudyProogramme()
        {
            Course = new HashSet<Course>();
            Creator = new HashSet<Creator>();
        }

        public ObjectId ProgrammeId { get; set; }
        public string Programme { get; set; }

        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Creator> Creator { get; set; }
    }
}
