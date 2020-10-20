using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cms.ar.xarchitecture.de.Helper
{
    public static class Backend
    {
        public static string DatabaseHost { get; set; }
        public static string DatabaseRemotePort { get; set; }
        public static string DatabaseName { get; set; }
        public static string DatabaseUser { get; set; }
        public static string DatabasePassword { get; set; }

        static Backend()
        {
            //define variables!
            DatabaseHost = Environment.GetEnvironmentVariable("");
            DatabaseRemotePort = Environment.GetEnvironmentVariable("");
            DatabaseName = Environment.GetEnvironmentVariable("");
            DatabaseUser = Environment.GetEnvironmentVariable("");
            DatabasePassword = Environment.GetEnvironmentVariable("");
        }     
        
        public static string GetDatabaseConnectionString()
        {
            return "mongodb://" 
                + DatabaseUser + ":" 
                + DatabasePassword + "@" 
                + DatabaseHost + ":" 
                + DatabaseRemotePort;
        }
        
        public static void SaveToFilesystem(IFileInfo file)
        { 
            // put write to fs function here
            //possibly overload for different parametres
        }
    }
}
