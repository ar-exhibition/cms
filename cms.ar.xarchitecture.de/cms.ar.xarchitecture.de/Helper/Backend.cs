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

        static Backend()
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
        
        public async static void SaveToFilesystem(IFormFile file, ContentType contentType)
        {          
            var path = Path.Combine(_mapContentTypeToFilePath(contentType), file.Name);

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
                case 1:
                    contentFolder = "assets";
                    break;
                case 2:
                    contentFolder = "marker";
                    break;
                case 3:
                    contentFolder = "thumbnails";
                    break;
                case 4:
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

        public static string MapFilenameToDownloadLink(ContentType contentType, string preamble ,string UUID)
        {
            string fullpath = "";

            switch ((int)contentType)
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

        private static string getFullyQualifiedFilename(string filename)
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

        public static AssetTypes getAssetTypeFromFilename(string filename)
        {
            string extension = System.IO.Path.GetExtension(filename);
            string[] imageExtensions = { ".png", ".jpg", ".jpeg" };
            string[] videoExtensions = { ".mp4", ".mov" };
            if (imageExtensions.Contains(extension))
            {
                return AssetTypes.image;
            }
            else if (videoExtensions.Contains(extension))
            {
                return AssetTypes.video;
            }
            else
            {
                return AssetTypes.model;
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
            GLTFmodel = 1,
            USDZmodel = 2,
            image = 3,
            video = 4,
            pdf = 5,
            light = 6
        }
    }
}
