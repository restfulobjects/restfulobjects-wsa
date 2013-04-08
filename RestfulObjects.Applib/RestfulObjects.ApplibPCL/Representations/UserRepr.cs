using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace RestfulObjects.Applib
{
    //
    public class UserRepr : JsonRepr
    {
        public string UserName { get; set; }

        public List<String> Roles { get; set; }

        public string FriendlyName { get; set; }

        public string Email { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }


    }
}
