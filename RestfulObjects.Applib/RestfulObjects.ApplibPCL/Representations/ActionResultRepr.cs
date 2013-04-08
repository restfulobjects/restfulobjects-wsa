using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestfulObjects.Applib
{
    public class ActionResultRepr : JsonRepr
    {
        /// <summary>
        /// "object", "list", "scalar" or "void"
        /// </summary>
        public string ResultType { get; set; }

        /// <summary>
        /// Per the result type, may be cast to either ObjectRepr, ListRepr or ScalarRepr.
        /// </summary>
        /// <see cref="ResultType"/>
        public GenericRepr Result { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
