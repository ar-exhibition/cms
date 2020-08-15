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

        public IActionResult Upload()
        {
            return View();
        }
    }
}
