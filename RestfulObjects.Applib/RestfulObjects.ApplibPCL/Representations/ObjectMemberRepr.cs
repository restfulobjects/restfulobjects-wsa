using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace RestfulObjects.Applib
{
    public class ObjectMemberRepr : GenericRepr
    {
        public string Id { get; set; }

        /// <summary>
        /// "property", "collection" or "action"
        /// </summary>
        public string MemberType { get; set; }

        public string DisabledReason { get; set; }

        /// <summary>
        /// Populated for property and collection, not for actions.
        /// </summary>
        /// <see cref="MemberType"/>
        public GenericRepr Value { get; set; }

        /// <summary>
        /// Populated only for collections.
        /// </summary>
        /// <see cref="MemberType"/>
        public int? Size { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
