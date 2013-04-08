using System.Collections.Generic;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class DomainTypeActionParamRepr : JsonRepr
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public string FriendlyName { get; set; }
        public string Description { get; set; }

        public bool Optional { get; set; }
        public int? MaxLength { get; set; }
        public string Pattern { get; set; }
        public string Format { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
