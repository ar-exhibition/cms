using cms.ar.xarchitecture.de.cmsXARCH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;


namespace cms.ar.xarchitecture.de.Models
{
    public class Content
    {
        public List<Assets> Assets;
        public List<Anchors> Anchors;
        public List<Scenes> Scenes;

        public Content()
        {
            Assets = new List<Assets>();
            Anchors = new List<Anchors>();
            Scenes = new List<Scenes>();
        }     
    }

    public class Assets
    {
        public string AssetId { get; set; }
        public string AssetType { get; set; }
        public Creator Creator { get; set; }
        public Course Course { get; set; }
        public string AssetName { get; set; }
        public string GLTFLink { get; set; }
        public string USDZLink { get; set; }
        public string ThumbnailLink{ get; set; }
    }

    public class Anchors
    {
        public string AnchorId { get; set; }
        public string AssetId { get; set; }
        public float? Scale { get; set; }
    }

    public class Scenes
    {
        public string SceneId { get; set; }
        public string SceneName { get; set; }
        public string WorldMapLink { get; set; }
        public string WorldMapUUID { get; set; }
        public Marker Marker { get; set; }
    }

    public class Creator
    {
        public string Name { get; set; }
    }

    public class Course
    {
        public string CourseName { get; set; }
        public string Term { get; set; }
        public string StudyProgramme { get; set; }
        public string University { get; set;  }
    }

    public class Marker
    {
        public string MarkerFileUUID { get; set; }
        public string FileLink { get; set; }
    }
}
