using cms.ar.xarchitecture.de.cmsDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace cms.ar.xarchitecture.de.Models
{
    public class Content
    {
        public IEnumerable<assets> assets;
        public IEnumerable<anchors> anchors;
        public IEnumerable<scenes> scenes;

        public Content()
        {


        }

        public async Task<String> getJson(cmsDatabaseContext _context)
        {
            string result = "";
            IEnumerable<SceneAsset> _assets = await _context.SceneAsset.ToListAsync();
            IEnumerable<Anchor> _anchors = await _context.Anchor.ToListAsync();
            IEnumerable<Scene> _scenes = await _context.Scene.ToListAsync();

            foreach (SceneAsset element in _assets)
            {
                if (element.AssetType == 1) //3d
                {
                    assets temp = new assets();

                    temp.assetId = element.AssetId;
                    temp.creator = new creator(element.Creator, _context);
                    temp.course = new course(element.CourseName, _context);
                    temp.name = element.Name;
                    temp.link = element.Link;
                    temp.thumbnail = element.Thumbnail;
                    temp.assetType = "3d";

                    assets.Append<assets>(temp);
                }

                else if(element.AssetType == 5) //light
                {
                    light temp = new light();

                    temp.type = element.Type;
                    temp.power = element.Power;
                    temp.color = element.Color;

                    assets.Append<light>(temp);
                }

            }

            foreach (Anchor element in _anchors)
            {
                anchors temp = new anchors();
                anchors.Append<anchors>(temp);

            }

            foreach (Scene element in _scenes)
            {
                scenes temp = new scenes();
                scenes.Append<scenes>(temp);

            }

            result = JsonConvert.SerializeObject(this, Formatting.Indented);
            return result;
        }
    }

    public class assets
    {
        public int assetId { get; set; }
        public creator creator { get; set; }
        public course course { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string thumbnail { get; set; }
        public string assetType { get; set; }
    }

    public class anchors
    {
        public int anchorId { get; set; }
        public float scale { get; set; }
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
            Creator record =  _context.Creator.Find(id);
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

    public class light
    {
        public string type { get; set; }
        public string power { get; set; }
        public string color { get; set; }
    }

    public class marker
    {
        public string name { get; set; }
        public string link { get; set; }
    }
}
