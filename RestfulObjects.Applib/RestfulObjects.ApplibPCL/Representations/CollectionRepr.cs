using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestfulObjects.Spec;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class CollectionRepr : JsonRepr
    {
        public string Id { get; set; }

        public List<LinkRepr> Value { get; set; }

        public string DisabledReason;

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
