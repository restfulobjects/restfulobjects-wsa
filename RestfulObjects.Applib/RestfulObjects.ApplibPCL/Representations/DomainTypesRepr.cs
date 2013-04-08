using System.Collections.Generic;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class DomainTypesRepr : JsonRepr
    {
        public List<LinkRepr> Value { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
