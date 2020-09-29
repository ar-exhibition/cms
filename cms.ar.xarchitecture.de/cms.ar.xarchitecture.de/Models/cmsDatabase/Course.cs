﻿using System;
using System.Collections.Generic;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class Course
    {
        public Course()
        {
            SceneAsset = new HashSet<SceneAsset>();
        }

        public int CourseId { get; set; }
        public string Programme { get; set; }
        public string CourseName { get; set; }
        public string Term { get; set; }

        public virtual Studies ProgrammeNavigation { get; set; }
        public virtual ICollection<SceneAsset> SceneAsset { get; set; }
    }
}