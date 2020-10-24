﻿using cms.ar.xarchitecture.de.cmsXARCH;
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
        public List<assets> assets;
        public List<anchors> anchors;
        public List<scenes> scenes;

        public Content()
        {
            assets = new List<assets>();
            anchors = new List<anchors>();
            scenes = new List<scenes>();
        }     
    }

    public class assets
    {
        public int assetId { get; set; }
        public string assetType { get; set; }
    }

    public class sceneassets : assets
    {
        public creator creator { get; set; }
        public course course { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string thumbnail { get; set; }
    }

    public class lightassets : assets
    {
        public string type { get; set; }
        public string power { get; set; }
        public string color { get; set; }
    }

    public class anchors
    {
        public string anchorId { get; set; }
        public int assetId { get; set; }
        public float? scale { get; set; }
    }

    public class scenes
    {
        public int sceneId { get; set; }
        public string name { get; set; }
        public string worldMapLink { get; set; }
        public string worldMapUUID { get; set; }
        public marker marker { get; set; }
    }

    public class creator
    {
        public creator(int? id, cmsXARCHContext _context)
        {
            Creator record = _context.Creator.Find(id);
            this.name = record.Creator1;
            this.studies = record.Programme;
        }

        public string name { get; set; }
        public string studies { get; set; }
    }

    public class course
    {
        public course(int? id, cmsXARCHContext _context)
        {
            Course record = _context.Course.Find(id);
            this.name = record.Course1;
            this.term = record.Term;
        }
        public string name { get; set; }
        public string term { get; set; }
    }

    public class marker
    {
        public marker(string _name, string _link)
        {
            name = _name;
            link = _link;
        }
        public string name { get; set; }
        public string link { get; set; }
    }
}