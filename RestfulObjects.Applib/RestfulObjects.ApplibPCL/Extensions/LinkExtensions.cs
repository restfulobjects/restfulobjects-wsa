using System.Collections.Generic;
using System.Linq;
using System;
using RestfulObjects.Spec;

namespace RestfulObjects.Applib
{
    public static class LinkExtensions
    {
        #region Single Link
        public static T Follow<T>(this LinkRepr link, ROClient linkFollower, object args = null) where T : JsonRepr, new()
        {
            var httpMethod = HttpMethod.Lookup(link.Method);
            return httpMethod.Follow<T>(linkFollower, link.Href, args);
        }
        #endregion
        
    }
}
