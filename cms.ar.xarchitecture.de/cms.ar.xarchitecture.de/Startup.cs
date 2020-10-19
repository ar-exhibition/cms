using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using cms.ar.xarchitecture.de.cmsXARCH;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using MongoDB.Driver;

namespace cms.ar.xarchitecture.de
{
    public class Startup
    {
        cmsConnectionOptions _options;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _options = new cmsConnectionOptions();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            cmsConnectionOptions _options = new cmsConnectionOptions();

            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                return new MongoClient(_options.GetConnectionString());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            var wrProvider = new PhysicalFileProvider(env.WebRootPath);
            var ctProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "static"));

            var compositeProvider = new CompositeFileProvider(wrProvider, ctProvider);

            var extensionProvider = new FileExtensionContentTypeProvider();


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = compositeProvider,
                RequestPath = "",
                ContentTypeProvider = extensionProvider,
                ServeUnknownFileTypes = true
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=About}/{id?}");

                endpoints.MapControllerRoute(
                    name: "upload",
                    pattern: "{controller=SceneAssets}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "adminPanel",
                    pattern: "{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "CRUD_Scene",
                    pattern: "{controller=Scenes}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "CRUD_Studies",
                    pattern: "{controller=Studies}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "CRUD_Course",
                    pattern: "{controller=Courses}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Model Viewer",
                    pattern: "{controller=ModelViewer}/{action=Index}");
                    
            });
        }
    }

    public class cmsConnectionOptions
    {
        public cmsConnectionOptions()
        {
            ServerAdress = Environment.GetEnvironmentVariable("DB_SERVER");
            Port = Environment.GetEnvironmentVariable("DB_PORT");
            Database = Environment.GetEnvironmentVariable("DB_NAME");
            User = Environment.GetEnvironmentVariable("DB_USER");
            Password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        }

        private string ServerAdress { get; set; }
        private string Port { get; set; }
        private string Database { get; set; }
        private string User { get; set; }
        private string Password { get; set; }

        public string GetConnectionString()
        {
            //return "server=" + ServerAdress + ";" 
            //    + "port=" + Port + ";" 
            //    + "user=" + User + ";" 
            //    + "password=" + Password + ";" 
            //    + "database=" + Database;
            return "INSERT CONNECTION STRING HERE IN A SECURE WAY";
        }
    }
}
