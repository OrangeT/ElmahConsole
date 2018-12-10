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
        public IEnumerable<ElmahError> Errors()
        {
            var list = new List<ElmahError>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command =
                new SqlCommand(
                    "SELECT TOP 10 errorId, application, host, type, source, message, [user], statusCode, timeUtc, allXml FROM ELMAH_Error ORDER BY TimeUtc DESC")
            )
            {
                command.Connection = connection;
                connection.Open();

                //Set up serializer
                var serializer = new XmlSerializer(typeof(ElmahErrorXml));

                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        list.Add(new ElmahError
                        {
                            ErrorId = reader.GetGuid(reader.GetOrdinal("errorId")),
                            Application = reader.GetString(reader.GetOrdinal("application")),
                            Host = reader.GetString(reader.GetOrdinal("host")),
                            Type = reader.GetString(reader.GetOrdinal("type")),
                            Source = reader.GetString(reader.GetOrdinal("source")),
                            Message = reader.GetString(reader.GetOrdinal("message")),
                            User = reader.GetString(reader.GetOrdinal("user")),
                            StatusCode = reader.GetInt32(reader.GetOrdinal("statusCode")),
                            TimeUtc = reader.GetDateTime(reader.GetOrdinal("timeUtc")),
                            AllXml = reader.GetString(reader.GetOrdinal("allXml")),
                            DecodedXml = (ElmahErrorXml)serializer.Deserialize(
                                new StringReader(reader.GetString(reader.GetOrdinal("allXml")))) // Deserialize XML
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return list;
        }

    }

    public class ElmahError
    {
        public Guid ErrorId { get; set; }

        public string Application { get; set; }

        public string Host { get; set; }

        public string Type { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public int StatusCode { get; set; }

        public DateTime TimeUtc { get; set; }

        public string AllXml { get; set; }
        public ElmahErrorXml DecodedXml { get; set; }
    }

    [Serializable()]
    [XmlRoot("error")]
    public class ElmahErrorXml
    {
        [XmlAttribute("application")]
        public string Application { get; set; }

        [XmlAttribute("host")]
        public string Host { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("message")]
        public string Message { get; set; }

        [XmlAttribute("source")]
        public string Source { get; set; }

        [XmlAttribute("detail")]
        public string Detail { get; set; }

        [XmlAttribute("user")]
        public string User { get; set; }

        [XmlAttribute("time")]
        public DateTime Time { get; set; }

        [XmlAttribute("statusCode")]
        public int StatusCode { get; set; }

        // public Dictionary<string, string> ServerVariables { get; set; }

        // public Dictionary<string, string> QueryString { get; set; }

        // public Dictionary<string, string> Cookies { get; set; }
    }

}