using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cms.ar.xarchitecture.de.cmsXARCH;
using cms.ar.xarchitecture.de.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace cms.ar.xarchitecture.de.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly cmsXARCHContext _context;
        string host;
        string prot;

        public ContentController(cmsXARCHContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            host = httpContextAccessor.HttpContext.Request.Host.Value;
            prot = httpContextAccessor.HttpContext.Request.Scheme;
        }

        // GET: api/Content
        [HttpGet]
        public async Task<String> Get()
        {
            return await getContent(_context);
        }

        // GET api/<APIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<APIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<APIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<APIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {            
        }

        public async Task<String> getContent(cmsXARCHContext _context)
        {

            Content content = new Content();

            IEnumerable<SceneAsset> _assets = await _context.SceneAsset.ToListAsync();
            IEnumerable<Anchor> _anchors = await _context.Anchor.ToListAsync();
            IEnumerable<Scene> _scenes = await _context.Scene.ToListAsync();

            foreach (SceneAsset element in _assets)
            {
                sceneassets temp = new sceneassets();

                temp.assetId = element.AssetId;
                temp.creator = new creator(element.Creator, _context);
                temp.course = new course(element.Course, _context);
                temp.name = element.AssetName;
                temp.link = mapFilenameToDownloadLink(RessourceType.asset, element.FileUuid);
                temp.linkUSDZ = mapFilenameToUSDZDownloadLink(RessourceType.asset, element.FileUuid);
                temp.thumbnail = mapFilenameToDownloadLink(RessourceType.thumbnail, element.ThumbnailUuid); //name of the thumbnail
                temp.assetType = mapAssetType((int) getAssetTypeFromFilename(element.FileUuid));

                content.assets.Add(temp);
            }

            foreach (Anchor element in _anchors)
            {
                anchors temp = new anchors();

                temp.anchorId = element.AnchorId;
                temp.assetId = element.AssetId;
                temp.scale = element.Scale;

                content.anchors.Add(temp);
            }

            foreach (Scene element in _scenes)
            {
                scenes temp = new scenes();

                temp.sceneId = element.SceneId;
                temp.name = element.SceneName;
                temp.worldMapLink = mapFilenameToDownloadLink(RessourceType.worldmap, element.FileUuid);
                temp.worldMapUUID = element.FileUuid;
                temp.marker = new marker(element.MarkerUuid, mapFilenameToDownloadLink(RessourceType.marker, element.MarkerUuid));

                content.scenes.Add(temp);
            }

            return JsonConvert.SerializeObject(content, Formatting.Indented);
        }
        string mapFilenameToDownloadLink(RessourceType ressourceType, string filename)
        {
            string controllerPath = prot + "://" + host + "/content/";
            string fullpath = "";

            switch ((int)ressourceType)
            {
                case (int)RessourceType.asset:
                    fullpath = controllerPath + "assets/" + filename;
                    break;
                case (int)RessourceType.marker:
                    fullpath = controllerPath + "marker/" + getFullyQualifiedFilename(filename);
                    break;
                case (int)RessourceType.thumbnail:
                    fullpath = controllerPath + "thumbnails/" + filename + ".png"; //are always png
                    break;
                case (int)RessourceType.worldmap:
                    fullpath = controllerPath + "worldmaps/" + filename;
                    break;
                default:
                    fullpath = "";
                    break;
            }
            return fullpath;
        }
        string mapFilenameToUSDZDownloadLink(RessourceType ressourceType, string filename)
        {
            string controllerPath = prot + "://" + host + "/content/";
            string fullpath = null;

            switch ((int)ressourceType)
            {
                case (int)RessourceType.asset:
                    string usdzName = null;
                    if (filename.EndsWith(".glb")) {
                        usdzName = filename.Replace(".glb", ".usdz");
                    } else if (filename.EndsWith(".gltf")) {
                        usdzName = filename.Replace(".gltf", ".usdz");
                    }
                    if (usdzName != null && System.IO.File.Exists("/app/static/content/assets/" + usdzName)) {
                        fullpath = controllerPath + "assets/" + usdzName;;
                    }
                    break;
                default:
                    fullpath = null;
                    break;
            }
            return fullpath;
        }

        string getFullyQualifiedFilename(string filename)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "static", "content", "marker");

                String[] files = Directory.GetFiles(path);

                foreach (String file in files)
                {
                    if (file.Contains(filename))
                    {
                        string[] tmp = file.Split(".");
                        return filename + "." + tmp.Last();
                    }
                }
            }
            catch
            {
                Console.WriteLine("whoops...");
                filename = "notfound"; //put some default file here prb. Provides static link to non-existing scene or so
            }


            return filename + ".png"; //defined behaviour
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

        AssetTypes getAssetTypeFromFilename(string filename) {
            string extension = System.IO.Path.GetExtension(filename);
            string[] imageExtensions = {".png", ".jpg", ".jpeg"};
            string[] videoExtensions = {".mp4", ".mov"};
            if (imageExtensions.Contains(extension)) {
                return AssetTypes.image;
            } else if (videoExtensions.Contains(extension)) {
                return AssetTypes.video;
            } else {
                return AssetTypes.model;
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

        private enum RessourceType
        {
            asset,
            worldmap,
            marker,
            thumbnail
        }
    }
}
