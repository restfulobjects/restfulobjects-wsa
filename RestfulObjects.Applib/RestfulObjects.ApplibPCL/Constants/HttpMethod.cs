using System.Collections.Generic;
using RestfulObjects.Applib;

namespace RestfulObjects.Spec
{
    public class HttpMethod
    {
        private static readonly Dictionary<string, HttpMethod> httpMethodsByName = new Dictionary<string, HttpMethod>();

        public static readonly HttpMethod Get = new HttpMethod("GET");
        public static readonly HttpMethod Put = new HttpMethod("PUT");
        public static readonly HttpMethod Post = new HttpMethod("POST");
        public static readonly HttpMethod Delete = new HttpMethod("DELETE");
        

        public static HttpMethod Lookup(string methodName)
        {
            return httpMethodsByName[methodName];
        }

        private HttpMethod(string methodName)
        {
            MethodName = methodName;
            httpMethodsByName[methodName] = this;
        }

        public string MethodName { get; set; }

        public T Follow<T>(ROClient roClient, string uri, object args) where T : JsonRepr, new()
        {
            if (this == Get)
            {
                return roClient.Get<T>(uri, args);                
            }
            if (this == Post)
            {
                return roClient.Post<T>(uri, args);
            }
            if (this == Put)
            {
                return roClient.Put<T>(uri, args);
            }
            if (this == Delete)
            {
                return roClient.Delete<T>(uri, args);
            }
            return null;
        }

        public bool ArgInQueryString()
        {
            return this == Get || this == Delete;
        }

        public bool ArgInBody()
        {
            return this == Post || this == Put;
        }
    }
}
