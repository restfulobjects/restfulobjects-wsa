using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulObjects.Spec
{
    public class Resource
    {
        public static readonly Resource HomePage = new Resource("/");
        public static readonly Resource User = new Resource("/user");
        public static readonly Resource Services = new Resource("/services");
        public static readonly Resource Version = new Resource("/version");
        public static readonly Resource ObjectsOfType = new Resource("/objects/{domainType}");
        public static readonly Resource DomainObject = new Resource("/objects/{domainType}/{instanceId}");
        public static readonly Resource DomainService = new Resource("/services/{serviceId}");

        public static readonly Resource Property =
            new Resource("/objects/{domainType}/{instanceId}/properties/{propertyId}");

        public static readonly Resource Collection =
            new Resource("/objects/{domainType}/{instanceId}/collections/{collectionId}");

        public static readonly Resource DomainServiceAction = new Resource("/services/{serviceId}/actions/{actionId}");

        public static readonly Resource DomainObjectAction =
            new Resource("/objects/{domainType}/{instanceId}/actions/{actionId}");

        public static readonly Resource DomainServiceActionInvoke =
            new Resource("/services/{serviceId}/actions/{actionId}/invoke");

        public static readonly Resource DomainObjectActionInvoke =
            new Resource("/objects/{domainType}/{instanceId}/actions/{actionId}/invoke");

        public static readonly Resource DomainTypes = new Resource("/domain-types");
        public static readonly Resource DomainType = new Resource("/domain-types/{domainType}");

        public static readonly Resource DomainTypeProperty =
            new Resource("/domain-types/{domainType}/properties/{propertyId}");

        public static readonly Resource DomainTypeCollection =
            new Resource("/domain-types/{domainType}/collections/{collectionId}");

        public static readonly Resource DomainTypeAction = new Resource("/domain-types/{domainType}/actions/{actionId}");

        public static readonly Resource DomainTypeActionParam =
            new Resource("/domain-types/{domainType}/actions/{actionId}/params/{paramName}");

        public static readonly Resource DomainTypeActionInvoke =
            new Resource("/domain-types/{domainType}/type-actions/{typeactionId}/invoke");

        internal readonly string _uriTemplateStr;
        internal readonly string _uriStr;

        private Resource(string uriTemplateStr)
        {
            _uriTemplateStr = uriTemplateStr.Contains("{") ? uriTemplateStr : null;
            _uriStr = _uriTemplateStr != null ? null : uriTemplateStr;
        }
    }
}

