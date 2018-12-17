using System;

namespace elmahconsole.lib.Models
{

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
}