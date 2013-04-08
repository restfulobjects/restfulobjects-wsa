using System.Collections.Generic;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class DomainTypeRepr : JsonRepr
    {
        public string Name { get; set; }
        public string DomainType { get; set; }
        public string FriendlyName { get; set; }
        public string PluralName { get; set; }
        public string Description { get; set; }
        public bool IsService { get; set; }

        public Dictionary<string, GenericRepr> TypeActions { get; set; }
        public Dictionary<string, GenericRepr> Members { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
