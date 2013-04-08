using System.Collections.Generic;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class DomainTypeCollectionRepr : JsonRepr
    {
        public string Id { get; set; }
        public string FriendlyName { get; set; }
        public string PluralForm { get; set; }
        public string Description { get; set; }
        
        public string MemberOrder { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
