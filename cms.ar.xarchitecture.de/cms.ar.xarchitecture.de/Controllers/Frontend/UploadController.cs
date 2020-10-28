using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Vlingo.UUID;
using cms.ar.xarchitecture.de.cmsXARCH;
using System.Collections;
using cms.ar.xarchitecture.de.Models.Wrapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using cms.ar.xarchitecture.de.Models.cmsXARCH;
using MongoDB.Bson;
using cms.ar.xarchitecture.de.Helper;


namespace cms.ar.xarchitecture.de.Controllers
{
    public class UploadController : Controller
    {
        NameBasedGenerator uuidCreator;
        private readonly IFileProvider fileProvider;
        private IMongoCollection<Asset> _assets;
        private IMongoCollection<University> _unis;
        private IMongoCollection<Course> _courses;
        private IMongoCollection<StudyProgramme> _studies;
        private IMongoCollection<Creator> _creators;

        private HttpClient client = new HttpClient();

        public UploadController(IFileProvider fileProvider, IMongoClient client)
        {
            this.fileProvider = fileProvider;
            uuidCreator = new NameBasedGenerator(HashType.SHA1);

            var database = client.GetDatabase("cmsXARCH");
            _assets = database.GetCollection<Asset>("Assets");
            _unis = database.GetCollection<University>("Universities");
            _courses = database.GetCollection<Course>("Courses");
            _studies = database.GetCollection<StudyProgramme>("StudyProgrammes");
            _creators = database.GetCollection<Creator>("Creators");
        }

        // GET: Upload/Create
        public IActionResult Create()
        { 
            ViewData["University"] = new SelectList(_unis.AsQueryable().Select(u => u.UniversityName).ToList());
            ViewData["StudyProgramme"] = new SelectList(_studies.AsQueryable().Select(s => s.ProgrammeName).ToList());                      

            //combine term and course in a single string
            List<String> cv = new List<String>();
            var cl = _courses.AsQueryable().Where(c => true).ToList();

            foreach (Course course in cl)
            {
                cv.Add(new String(course.Term + " " + course.CourseName));
            }

            //Console.WriteLine(cv.ToList());
            ViewData["CourseName"] = new SelectList(cv.ToList());

            return View();
        }

        [HttpPost]
        [RequestSizeLimit(62_914_560)]
        public async Task<IActionResult> UploadFile(AssetSubmissionValues values)
        {
            string[] splitUserInputCourse = splitCourse(values.Course);

            //Course course = await _courses.AsQueryable().Where(c => c.CourseName.Contains(values.Course) && c.Term.Contains(values.Course)).FirstAsync();
            Course course = await _courses.AsQueryable().Where(c => c.CourseName.Equals(splitUserInputCourse[1])).FirstAsync();
            Asset newAsset = new Asset();
            newAsset._id = new ObjectId();

            if (values.AssetFile == null || values.AssetFile.Length == 0)
                return Content("file not selected");

            string extension = Path.GetExtension(values.AssetFile.FileName);
            string filename = uuidCreator.GenerateGuid(values.AssetFile.FileName + DateTime.Now) + extension;
            var thumbnailUUID = uuidCreator.GenerateGuid(values.ThumbnailBase64 + DateTime.Now);
            string thumbnailFilename = thumbnailUUID + ".png";

            string dir = Directory.GetCurrentDirectory();

            var path = Path.Combine(
                        dir, "static", "content" ,"assets",
                        filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await values.AssetFile.CopyToAsync(stream);
            }

            // prepare body for convert request
            var formValues = new Dictionary<string, string> {
                { "filename", filename }
            };

            // create json string from dictionary
            var content = new StringContent(JsonSerializer.Serialize(formValues), Encoding.UTF8, "application/json");

            // post to converter service
            client.PostAsync("http://gltf-to-usdz-service:3000/local-convert", content);
            
            Creator creator = await _creators.AsQueryable().
                Where(c => c.CreatorName == values.Creator).
                FirstOrDefaultAsync();


            if (creator == default)
            {
                creator = new Creator { _id = new ObjectId(), CreatorName = values.Creator, Assets = new List<ObjectId>() };
                await _creators.InsertOneAsync(creator);
            }

            //integrate into backend functionality
            var thumbPath = Path.Combine(
            dir, "static", "content", "thumbnails",
            thumbnailFilename);

            String base64Str = values.ThumbnailBase64;
            base64Str = base64Str.Split(",")[1];
            byte[] bytes = Convert.FromBase64String(base64Str);

            using (var stream = new FileStream(thumbPath, FileMode.Create))
            {
                await stream.WriteAsync(bytes);
            }
            // ...

            newAsset.Creator = creator._id;
            newAsset.Course = course._id;
            newAsset.AssetName = values.AssetName;
            newAsset.AssetFilename = filename; //with uuid
            newAsset.AssetType = Backend.getAssetTypeFromFilename(filename);
            newAsset.ExternalLink = null;
            newAsset.ThumbnailFilename = Convert.ToString(thumbnailUUID) + ".png";
            newAsset.CreationDate = DateTime.Now;
            newAsset.Deleted = false;            

            if (ModelState.IsValid)
                await _assets.InsertOneAsync(newAsset);

            //add the new asset to the creators asset property
            creator.Assets.Add(newAsset._id);
            var crUpdate = Builders<Creator>.Update.Set(s => s.Assets, creator.Assets);
            var crOptions = new UpdateOptions();
            crOptions.IsUpsert = true;
            await _creators.UpdateOneAsync(c => c._id == creator._id, crUpdate, crOptions);

            //add the new asset to the courses asset property
            course.Assets.Add(newAsset._id);
            var coUpdate = Builders<Course>.Update.Set(s => s.Assets, course.Assets);
            var coOptions = new UpdateOptions();
            coOptions.IsUpsert = true;
            await _courses.UpdateOneAsync(c => c._id == course._id, coUpdate, coOptions);

            return RedirectToAction("About", "Home");
        }

        private string[] splitCourse(string value)
        {
            //dirty, huh?
            String[] arr = value.Split(" ");
            String term = arr[0];
            String courseName = "";

            for (int i = 1; i < arr.Length; i++)
            {
                courseName += " " + arr[i];
            }

            courseName = courseName.Trim();

            return new string[] { term, courseName};
        }
    }
}
