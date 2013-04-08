using System.Collections.Generic;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class VersionRepr : JsonRepr
    {
        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
