using System.Collections.Generic;
using RestfulObjects.Spec;

namespace RestfulObjects.Applib
{
    public interface ROClient
    {
        /// <summary>
        /// Submit a request to an arbitrary URL, eg as obtained by following a link
        /// </summary>
        RORequest RORequestTo(string path);

        /// <summary>
        /// Represents a request to a templated URL
        /// </summary>
        RORequest RORequestTo(Resource resource, params string[] pathElements);

        /// <summary>
        /// Represents a request to a templated URL
        /// </summary>
        RORequest RORequestTo(Resource resource, Dictionary<string, string> pathElementMap);

        T Execute<T>(HttpMethod httpMethod, RORequest roRequest, object args = null) where T : JsonRepr, new();


        //
        // convenience; arbitrary requests
        //
        T Get<T>(string path, object args = null) where T : JsonRepr, new();
        T Post<T>(string uri, object args = null) where T : JsonRepr, new();
        T Put<T>(string uri, object args = null) where T : JsonRepr, new();
        T Delete<T>(string uri, object args = null) where T : JsonRepr, new();



        //
        // convenience, type-safe templated URLs (GET only)
        //

        /// <summary>
        /// rospec-5
        /// </summary>
        HomePageRepr HomePage();

        /// <summary>
        /// rospec-6
        /// </summary>
        UserRepr User();

        /// <summary>
        /// rospec-7
        /// </summary>
        ListRepr Services();

        /// <summary>
        /// rospec-8
        /// </summary>
        VersionRepr Version();


        /// <summary>
        /// rospec-14
        /// </summary>
        ObjectRepr DomainObject(string domainType, string instanceId);

        /// <summary>
        /// rospec-14
        /// </summary>
        T DomainObject<T>(string domainType, string instanceId) where T : ObjectRepr, new();


        /// <summary>
        /// rospec-15
        /// </summary>
        ObjectRepr DomainService(string serviceName);

        /// <summary>
        /// rospec-15
        /// </summary>
        T DomainService<T>(string serviceName) where T : ObjectRepr, new();


        /// <summary>
        /// rospec-16
        /// </summary>
        PropertyRepr Property(string domainType, string instanceId, string propertyName);

        /// <summary>
        /// rospec-16
        /// </summary>
        T Property<T>(string domainType, string instanceId, string propertyName) where T : PropertyRepr, new();


        /// <summary>
        /// rospec-17
        /// </summary>
        CollectionRepr Collection(string domainType, string instanceId, string collectionName);

        /// <summary>
        /// rospec-17
        /// </summary>
        T Collection<T>(string domainType, string instanceId, string collectionName) where T : CollectionRepr, new();


        /// <summary>
        /// rospec-18 (objects)
        /// </summary>
        ActionRepr DomainObjectAction(string domainType, string instanceId, string actionName);
        /// <summary>
        /// rospec-18 (objects)
        /// </summary>
        T DomainObjectAction<T>(string domainType, string instanceId, string actionName) where T:ActionRepr, new();

        /// <summary>
        /// rospec-18 (services)
        /// </summary>
        ActionRepr DomainServiceAction(string serviceId, string actionName);

        /// <summary>
        /// rospec-18 (services)
        /// </summary>
        T DomainServiceAction<T>(string serviceId, string actionName) where T : ActionRepr, new ();


        /// <summary>
        /// rospec-21
        /// </summary>
        DomainTypesRepr DomainTypes();

        /// <summary>
        /// rospec-22
        /// </summary>
        DomainTypeRepr DomainType(string domainType);

        /// <summary>
        /// rospec-23
        /// </summary>
        DomainTypePropertyRepr DomainTypeProperty(string domainType, string propertyName);

        /// <summary>
        /// rospec-24
        /// </summary>
        DomainTypeCollectionRepr DomainTypeCollection(string domainType, string collectionName);

        /// <summary>
        /// rospec-25
        /// </summary>
        DomainTypeActionRepr DomainTypeAction(string domainType, string actionName);

        /// <summary>
        /// rospec-26
        /// </summary>
        DomainTypeActionParamRepr DomainTypeActionParam(string domainType, string actionName, string paramName);
    }
}
