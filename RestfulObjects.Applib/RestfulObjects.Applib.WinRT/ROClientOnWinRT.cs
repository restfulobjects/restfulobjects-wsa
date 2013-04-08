using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestfulObjects.Spec;
using System.IO;
using System.Runtime.Serialization.Json;
//using Newtonsoft.Json;

namespace RestfulObjects.Applib.WinRT
{
    public class ROClientOnWinRT : ROClientAbstractWithAutowire
    {

        public HttpClient HttpClient { get; private set; }

        public ROClientOnWinRT(string baseUrl)
            : base(baseUrl)
        {

            HttpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) };
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override T DoExecute<T>(RestfulObjects.Spec.HttpMethod httpMethod, RORequest roRequest, object args = null)
        {
            HttpResponseMessage response = null;
            if (httpMethod == RestfulObjects.Spec.HttpMethod.Get) 
            {
                var uri = roRequest.AsUriWithArgs(args);
                response = HttpClient.GetAsync(uri).Result;
            }
            
            if (httpMethod == RestfulObjects.Spec.HttpMethod.Post)
            {
                response = HttpClient.PostAsync(roRequest.AsUri(), roRequest.AsBody(args)).Result;

            }
            
            if (httpMethod == RestfulObjects.Spec.HttpMethod.Put)
            {
                throw new Exception("not yet implemented.");
            }
            
            if (httpMethod == RestfulObjects.Spec.HttpMethod.Delete)
            {
                response = HttpClient.DeleteAsync(roRequest.AsUriWithArgs(args)).Result;
            }
            
            var responseContent = response.Content;
            if (response.IsSuccessStatusCode)
            {
                return JsonRepr.FromString<T>(responseContent.ReadAsStringAsync().Result);
            }
            else
            {
                throw new Exception(String.Format("Bad Request, status code: {0}, content={1}" + response.StatusCode, responseContent));
            }

        }


    }

    public static class RORequestExtension
    {
        public static Uri AsUri(this RORequest roRequest)
        {
            var uriStr = roRequest.AsUriStr();
            return new Uri(uriStr);
        }
        public static Uri AsUriWithArgs(this RORequest roRequest, object args)
        {
            var uriStr = roRequest.AsUriStr();
            var argStr = AsStr(args);
            uriStr += (args != null)? "?" + argStr: "";
            return new Uri(uriStr);
        }
        public static HttpContent AsBody(this RORequest roRequest, object args)
        {
            var argStr = AsStr(args);
            var httpContent = new StringContent(argStr);
            httpContent.Headers.Add("Content-Type", "application/json");
            return httpContent;
        }

        private static string AsStr(object args)
        {
            if (args == null)
            {
                return null;
            }
            var sw = new StringWriter();
            new RestfulSerializer().Serialize(sw, args);
            var asStr = sw.ToString();
            return asStr;
        }
    }
}
