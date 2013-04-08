using System.Collections.Generic;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class ActionRepr : JsonRepr
    {
        public string Id { get; set; }
        public Dictionary<string, ParameterRepr> Parameters { get; set; }

        public string DisabledReason { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }

    }
}
