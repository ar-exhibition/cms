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

        public enum ContentType
        {
            Asset,
            Marker,
            Thumbnail,
            WorldMap
        }


    }
}
