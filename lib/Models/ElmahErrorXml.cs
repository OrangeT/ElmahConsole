using System;
using System.Xml.Serialization;

namespace elmahconsole.lib.Models
{

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
