using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;


namespace RestfulObjects.Applib
{
    public class LinkRepr : JsonRepr
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
        public string Type { get; set; }

        public Dictionary<string, GenericRepr> Arguments { get; set; }
    }
}
