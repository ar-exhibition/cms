using cms.ar.xarchitecture.de.cmsDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace cms.ar.xarchitecture.de.Models
{
    public class Content
    {
        //public IEnumerable<assets> assets;
        public List<assets> assets;
        //public IEnumerable<anchors> anchors;
        public List<anchors> anchors;
        //public IEnumerable<scenes> scenes;
        public List<scenes> scenes;

        public Content()
        {
            assets = new List<assets>();
            anchors = new List<anchors>();
            scenes = new List<scenes>();
        }

        public async Task<String> getJson(cmsDatabaseContext _context)
        {
            string result = "";
            IEnumerable<SceneAsset> _assets = await _context.SceneAsset.ToListAsync();
            IEnumerable<Anchor> _anchors = await _context.Anchor.ToListAsync();
            IEnumerable<Scene> _scenes = await _context.Scene.ToListAsync();

            foreach (SceneAsset element in _assets)
            {
                if (element.AssetType == (int)AssetTypes.light) //light
                {
                    lightassets temp = new lightassets();

                    temp.assetId = element.AssetId;
                    temp.type = element.Type;
                    temp.power = element.Power;
                    temp.color = element.Color;
                    temp.assetType = mapAssetType(element.AssetType);

                    //assets = assets.Append(temp);
                    assets.Add(temp);
                }

                else //3d and everything else except light
                {
                    sceneassets temp = new sceneassets();

                    temp.assetId = element.AssetId;
                    temp.creator = new creator(element.Creator, _context);
                    temp.course = new course(element.CourseName, _context);
                    temp.name = element.Name;
                    temp.link = element.Link;
                    temp.thumbnail = element.Thumbnail;
                    temp.assetType = mapAssetType(element.AssetType);

                    //assets = assets.Append(temp);
                    assets.Add(temp);
                }

            }

            foreach (Anchor element in _anchors)
            {
                anchors temp = new anchors();

                temp.anchorId = element.AnchorId;
                temp.assetId = element.AssetId;
                temp.scale = element.Scale;

                anchors.Add(temp);
            }

            foreach (Scene element in _scenes)
            {
                scenes temp = new scenes();

                temp.sceneId = element.SceneId;
                temp.name = element.Name;
                temp.worldMapLink = element.SceneFile;
                temp.marker = new marker(element.MarkerName, element.MarkerFile);

                //scenes = scenes.Append<scenes>(temp);
                scenes.Add(temp);

            }

            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        string mapAssetType(int? ID)
        {
            switch (ID)
            {
                case (int)AssetTypes.model:
                    return "3d";
                case (int)AssetTypes.image:
                    return "image";
                case (int)AssetTypes.video:
                    return "video";
                case (int)AssetTypes.pdf:
                    return "pdf";
                case (int)AssetTypes.light:
                    return "light";
                default:
                    return "";
            }
        }

        private enum AssetTypes
        {
            model = 1,
            image = 2,
            video = 3,
            pdf = 4,
            light = 5
        }
    }

    //public class JSONassets
    public class assets
    {
        public int assetId { get; set; }
        //
        //public creator creator { get; set; }
        //public course course { get; set; }
        //public string name { get; set; }
        //public string link { get; set; }
        //public string thumbnail { get; set; }
        //
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
        public marker marker { get; set; }
    }

    public class creator
    {
        public creator(int id, cmsDatabaseContext _context)
        {
            Creator record = _context.Creator.Find(id);
            this.name = record.Name;
            this.studies = record.Programme;
        }

        public string name { get; set; }
        public string studies { get; set; }
    }

    public class course
    {
        public course(int id, cmsDatabaseContext _context)
        {
            Course record = _context.Course.Find(id);
            this.name = record.CourseName;
            this.term = record.Term;
        }
        public string name { get; set; }
        public string term { get; set; }
    }

    //public class light
    //{
    //    public string type { get; set; }
    //    public string power { get; set; }
    //    public string color { get; set; }
    //}

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
