﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cms.ar.xarchitecture.de.cmsXARCH;
using cms.ar.xarchitecture.de.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

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
                temp.thumbnail = mapFilenameToDownloadLink(RessourceType.thumbnail, element.ThumbnailUuid); //name of the thumbnail
                temp.assetType = "3d"; //dirtyhack. implement a more permanent solution.

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
                temp.marker = new marker(element.MarkerUuid, mapFilenameToDownloadLink(RessourceType.marker, element.MarkerUuid));

                content.scenes.Add(temp);
            }

            return JsonConvert.SerializeObject(content, Formatting.Indented);
        }
        string mapFilenameToDownloadLink(RessourceType ressourceType, string filename)
        {
            string controllerPath = prot + "://" + host + "/Download/Get?";
            string fullpath = "";

            switch ((int)ressourceType)
            {
                case (int)RessourceType.asset:
                    fullpath = controllerPath + "type=assets&file=" + filename;
                    break;
                case (int)RessourceType.marker:
                    fullpath = controllerPath + "type=marker&file=" + filename;
                    break;
                case (int)RessourceType.thumbnail:
                    fullpath = controllerPath + "type=thumbnails&file=" + filename;
                    break;
                case (int)RessourceType.worldmap:
                    fullpath = controllerPath + "type=worldmaps&file=" + filename;
                    break;
                default:
                    fullpath = "";
                    break;
            }
            return fullpath;
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

        private enum RessourceType
        {
            asset,
            worldmap,
            marker,
            thumbnail
        }
    }
}
