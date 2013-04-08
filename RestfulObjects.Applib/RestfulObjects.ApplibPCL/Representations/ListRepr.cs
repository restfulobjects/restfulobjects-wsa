using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace RestfulObjects.Applib
{
    public class ListRepr : JsonRepr
    {
        public List<LinkRepr> Value { get; set; }
        
        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
