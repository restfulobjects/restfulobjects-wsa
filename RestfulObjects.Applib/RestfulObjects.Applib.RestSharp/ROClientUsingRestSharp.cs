using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using RestSharp;
using RestSharp.Deserializers;
using RestfulObjects.ApplibPCL.Exceptions;
using RestfulObjects.Spec;
using RestfulObjects.Applib.RestSharp.Extensions;

namespace RestfulObjects.Applib.RestSharp
{
    public class ROClientUsingRestSharp : ROClientAbstractWithAutowire
    {
        public RestClient RestClient { get; private set; }

        public ROClientUsingRestSharp(string baseUrl) : base(baseUrl)
        {
            RestClient = new RestClient { BaseUrl = baseUrl };
            RestClient.AddHandler("application/json", new JsonDeserializerSupportingGenericRepr());
        }

        public override T DoExecute<T>(HttpMethod httpMethod, RORequest roRequest, object args = null)
        {
            return ExecuteRequest<T>(roRequest.AsRestRequest(httpMethod, args));
        }

        private T ExecuteRequest<T>(IRestRequest restRequest) where T : JsonRepr, new()
        {
            if (Credentials != null)
            {
                restRequest.Credentials = Credentials;
            }
            
            var restResponse = RestClient.Execute<T>(restRequest);

            return HandleResponse(restResponse);
        }

        private static T HandleResponse<T>(IRestResponse<T> restResponse) where T : JsonRepr, new()
        {

            if (restResponse.ErrorException != null)
            {
                // handle any non-HTTP exception
                if ("text/html".Equals(restResponse.ContentType))
                {
                    // workaround for http://restfulobjects.codeplex.com/workitem/69; do nothing
                }
                else
                {
                    throw new ROUnderlyingException(restResponse.ErrorException);
                }
            }

            if (restResponse.ErrorMessage != null)
            {
                // handle any non-HTTP exception
                if ("text/html".Equals(restResponse.ContentType))
                {
                    // workaround for http://restfulobjects.codeplex.com/workitem/69; do nothing
                }
                else
                {
                    throw new ROUnderlyingException(restResponse.ErrorMessage);
                }
            }

            var statusCode = (int) restResponse.StatusCode;
            if (statusCode >= 200 && statusCode < 300)
            {
                return restResponse.Data;
            }
            if (statusCode >= 100 && statusCode < 200)
            {
                throw new ROException("Handling of 100-range http status code is not yet implemented");
            }
            // redirects (300 range) are handled automatically by Restsharp

            if (statusCode == 403)
            {
                throw new RODisabledException(WarningHeaderIfPresent(restResponse.Headers, "Disabled (no further information available)"));    
            }
            if (statusCode == 404)
            {
                throw new RONotFoundException(WarningHeaderIfPresent(restResponse.Headers, "Not found (no further information available)"));
            }
            if (statusCode == 422)
            {
                throw new ROValidationFailedException(WarningHeaderIfPresent(restResponse.Headers, "Validation failed (no further information available)"));
            }

            if (restResponse.ContentType.Contains("urn:org.restfulobjects:repr-types/error"))
            {
                throw new ROApplicationWarningException(WarningHeaderIfPresent(restResponse.Headers, "Application warning (no further information available)"));
            }

            // else
            throw new ROHttpException(restResponse.StatusCode, restResponse.ContentType, restResponse.Content);
        }

        private static string WarningHeaderIfPresent(IEnumerable<Parameter> headers, string fallback)
        {
            foreach (var header in headers)
            {
                if (header.Name.Equals("Warning"))
                {
                    return StripWarningPrefix(header.Value.ToString());
                }
            }
            return fallback;
        }

        // Warning header gets prefixed by '199 RestfulObjects'; strip
        private static string StripWarningPrefix(string str)
        {
            const string prefix = "199 RestfulObjects";
            return str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;
        }
    }

    public static class RORequestExtension
    {
        public static IRestRequest AsRestRequest(this RORequest roRequest, HttpMethod httpMethod, object args)
        {
            var uriStr = roRequest.AsUriStr();
            var argStr = AsStr(args);
            if (argStr != null && httpMethod.ArgInQueryString())
            {
                uriStr += "?" + argStr;
            }
            var uri = new Uri(uriStr);
            var request = new RestRequest(uri, httpMethod.RSMethod());
            if (argStr != null && httpMethod.ArgInBody())
            {
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("application/json", argStr, ParameterType.RequestBody);
            }
            return request;
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

    public class JsonDeserializerSupportingGenericRepr : IDeserializer
    {
        public CultureInfo Culture { get; set; }
        public string DateFormat { get; set; }
        public string Namespace { get; set; }
        public string RootElement { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            var content = response.Content;
            return JsonRepr.FromString<T>(content);
        }
    }

}
