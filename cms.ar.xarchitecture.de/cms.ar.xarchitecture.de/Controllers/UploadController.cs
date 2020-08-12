using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MySql.Data.MySqlClient;
using dto = cms.ar.xarchitecture.de.Models;

namespace cms.ar.xarchitecture.de.Controllers
{
    public class UploadController : Controller
    {
        private MySQLDatabase MySQLDatabase { get; set; }

        public UploadController(MySQLDatabase db)
        {
            this.MySQLDatabase = db;
        }

        [HttpPost]
        public void Upload(dto.Creator creator, dto.Course course, dto.Asset asset)
        {
            var cmd = this.MySQLDatabase.Connection.CreateCommand() as MySqlCommand;

            //do some sql magic here
            /*
             From tutorial:
            cmd.CommandText = @"UPDATE Tasks SET Completed = STR_TO_DATE(@Date, '%Y/%m/%d') WHERE TaskId = @TaskId;";
            cmd.Parameters.AddWithValue("@TaskId", input.TaskId);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy/MM/dd"));
             */

            var recs = cmd.ExecuteNonQuery();
        }

        public IActionResult Upload()
        {
            return View();
        }
    }
}
