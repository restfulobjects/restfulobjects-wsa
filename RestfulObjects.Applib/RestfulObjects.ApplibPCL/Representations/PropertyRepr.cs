using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestfulObjects.Spec;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class PropertyRepr : JsonRepr
    {
        public string Id { get; set; }

        public GenericRepr Value { get; set; }
        public List<GenericRepr> Choices { get; set; }

        public string DisabledReason { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
