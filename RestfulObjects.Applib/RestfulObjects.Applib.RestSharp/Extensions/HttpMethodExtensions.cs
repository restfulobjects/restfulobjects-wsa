using System;
using RestSharp;
using RestfulObjects.Spec;

namespace RestfulObjects.Applib.RestSharp.Extensions
{
    public static class HttpMethodExtensions
    {
        public static Method RSMethod(this HttpMethod httpMethod)
        {
            Method result;
            Enum.TryParse(httpMethod.MethodName, true, out result);
            return result;
        }
    }
}
