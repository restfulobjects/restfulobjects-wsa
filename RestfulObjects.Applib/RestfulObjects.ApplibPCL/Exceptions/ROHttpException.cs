using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace RestfulObjects.ApplibPCL.Exceptions
{
    public class ROHttpException : ROException
    {

        public ROHttpException(HttpStatusCode httpStatusCode, string contentType, string entity)
            : base("HTTP Exception")
        {
            HttpStatusCode = httpStatusCode;
            ContentType = contentType;
            Entity = entity;
        }

        public HttpStatusCode HttpStatusCode { get; private set; }
        public string ContentType { get; private set; }
        public string Entity { get; private set; }
    }
}
