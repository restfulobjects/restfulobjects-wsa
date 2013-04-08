using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestfulObjects.Spec;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class ObjectRepr : JsonRepr
    {
        /// <summary>
        /// Applies only when the representation is of a domain service
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// Applies only when the representation is of a domain object
        /// </summary>
        public string DomainType { get; set; }

        /// <summary>
        /// Applies only when the representation is of a domain object
        /// </summary>
        public string InstanceId { get; set; }

        public string Title { get; set; }

        public Dictionary<string, ObjectMemberRepr> Members { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }

    }
}
