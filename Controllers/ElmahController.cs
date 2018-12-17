using ElmahCore;
using ElmahCore.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;
using System.IO;
using elmahconsole.lib.Models;
using elmahconsole.lib.Sql;

namespace elmahconsole.Controllers
{
    [Route("api/[controller]")]
    public class ElmahController : Controller
    {
        private string _connectionString;

        public ElmahController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("elmah");
        }

        [HttpGet("[action]")]
        public ElmahErrorList Errors(int page = 0)
        {
            ElmahErrorList dto = new ElmahErrorList();

            using (var elmahSql = new ElmahSql(_connectionString))
            {
                    dto.Total = elmahSql.TotalErrors();
                    dto.Errors = elmahSql.ErrorList(page);
                    dto.Pages = (int)Math.Ceiling((decimal)dto.Total / ElmahSql.PAGE_SIZE);
                    dto.CurrentPage = page;
            }

            return dto;
        }
    }
}