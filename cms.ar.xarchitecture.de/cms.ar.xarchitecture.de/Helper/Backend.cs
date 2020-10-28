using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace cms.ar.xarchitecture.de.Helper
{
    public static class Backend
    {
        public static string DatabaseHost { get; set; }
        public static string DatabaseRemotePort { get; set; }
        public static string DatabaseName { get; set; }
        public static string DatabaseUser { get; set; }
        public static string DatabasePassword { get; set; }

        private static string _localDirectoryRoot {get; set;}
        private static string _localStaticRoot { get; set; }
        private static string _localContentRoot { get; set; }

        static Backend() //find way to inject stuff here
        {
            DatabaseHost = Environment.GetEnvironmentVariable("DATABASE_HOST");
            DatabaseRemotePort = Environment.GetEnvironmentVariable("DATABASE_REMOTE_PORT");
            DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
            DatabaseUser = Environment.GetEnvironmentVariable("DATABASE_USER");
            DatabasePassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

            _localDirectoryRoot = Directory.GetCurrentDirectory();

            //probably find some nicer way to make the config. Like an XML or so
            _localStaticRoot = "static";
            _localContentRoot = "content";
        }     
        
        public static string GetDatabaseConnectionString()
        {
            return "mongodb://" 
                + DatabaseUser + ":" 
                + DatabasePassword + "@" 
                + DatabaseHost + ":" 
                + DatabaseRemotePort;
        }
        
        public static async Task SaveToFilesystem(IFormFile file, ContentType contentType)
        {
            string path = Path.Combine(_mapContentTypeToFilePath(contentType), file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        private static string _mapContentTypeToFilePath(ContentType contentType)
        {
            string contentFolder = "";
            switch ((int)contentType)
            {
                case 0:
                    contentFolder = "assets";
                    break;
                case 1:
                    contentFolder = "marker";
                    break;
                case 2:
                    contentFolder = "thumbnails";
                    break;
                case 3:
                    contentFolder = "worldmaps";
                    break;
                default:
                    contentFolder = "assets";
                    break;
            }
            return Path.Combine(_localDirectoryRoot,
                                    _localStaticRoot,
                                    _localContentRoot, 
                                    contentFolder);
        }

        public static string MapFilenameToDownloadLink(ContentType contentType, string preamble, string filename)
        {
            string fullpath;
            string controllerPath = preamble + "/" 
                            + _localStaticRoot + "/" 
                            + _localContentRoot + "/"; 

            switch ((int)contentType)
            {
                case (int)ContentType.Asset:
                    fullpath = controllerPath + "assets/" + filename;
                    break;
                case (int)ContentType.Marker:
                    fullpath = controllerPath + "marker/" + filename;
                    break;
                case (int)ContentType.Thumbnail:
                    fullpath = controllerPath + "thumbnails/" + filename;
                    break;
                case (int)ContentType.WorldMap:
                    fullpath = controllerPath + "worldmaps/" + filename;
                    break;
                default:
                    fullpath = "";
                    break;
            }
            return fullpath;
        }

        public static string MapFilenameToUSDZDownloadLink(ContentType contentType, string preamble, string filename)
        {
            string fullpath;
            string controllerPath = preamble + "/" 
                            + _localStaticRoot + "/" 
                            + _localContentRoot + "/"; 

            switch ((int)contentType)
            {
                case (int)ContentType.Asset:
                    string usdzName = null;
                    // replace glb or gltf with usdz
                    if (filename.EndsWith(".glb")) {
                        usdzName = filename.Replace(".glb", ".usdz");
                    } else if (filename.EndsWith(".gltf")) {
                        usdzName = filename.Replace(".gltf", ".usdz");
                    }
                    // check if usdz file exists
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

        private static string mapAssetType(int? ID)
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

        public static string getAssetTypeFromFilename(string filename) //solve this with mime type on upload!
        {
            string extension = Path.GetExtension(filename);
            string[] imageExtensions = { ".png", ".jpg", ".jpeg" };
            string[] videoExtensions = { ".mp4", ".mov" };
            if (imageExtensions.Contains(extension))
            {
                return mapAssetType((int)AssetTypes.image);
            }
            else if (videoExtensions.Contains(extension))
            {
                return mapAssetType((int)AssetTypes.video);
            }
            else
            {
                return mapAssetType((int)AssetTypes.model);
            }
        }



        public enum ContentType
        {
            Asset,
            Marker,
            Thumbnail,
            WorldMap
        }

        public enum AssetTypes
        {
            model = 1,
            image = 2,
            video = 3,
            pdf = 4,
            light = 5
        }
    }
}
