using System.Collections.Generic;

namespace elmahconsole.lib.Models
{
    public class ElmahErrorList
    {
        public int Total { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public List<ElmahError> Errors { get; set; }
    }

}