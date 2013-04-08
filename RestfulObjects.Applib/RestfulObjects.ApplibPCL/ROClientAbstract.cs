using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using RestfulObjects.Spec;

namespace RestfulObjects.Applib
{
    public abstract class ROClientAbstract : ROClient
    {
        public ICredentials Credentials { get; set; }

        protected ROClientAbstract(string baseUrl)
        {
            BaseUri = new Uri(baseUrl);
        }

        protected Uri BaseUri { get; private set; }


        //
        // factories
        //

        public RORequest RORequestTo(Resource resource, params string[] pathElements)
        {
            return RORequest.To(BaseUri, resource, pathElements);
        }
        public RORequest RORequestTo(Resource resource, Dictionary<string, string> pathElementMap)
        {
            return RORequest.To(BaseUri, resource, pathElementMap);
        }
        public RORequest RORequestTo(string path)
        {
            return RORequest.To(path);
        }



        /// <summary>
        /// mandatory hook method
        /// </summary>
        public abstract T Execute<T>(HttpMethod httpMethod, RORequest roRequest, object args = null)
            where T : JsonRepr, new();



        //
        // convenience
        //

        public T Get<T>(string path, object args = null) where T : JsonRepr, new()
        {
            return Execute<T>(HttpMethod.Get, RORequest.To(path), args);
        }

        public T Delete<T>(string path, object args = null) where T : JsonRepr, new()
        {
            return Execute<T>(HttpMethod.Delete, RORequest.To(path), args);
        }

        public T Post<T>(string path, object args = null) where T : JsonRepr, new()
        {
            return Execute<T>(HttpMethod.Post, RORequest.To(path), args);
        }

        public T Put<T>(string path, object args = null) where T : JsonRepr, new()
        {
            return Execute<T>(HttpMethod.Put, RORequest.To(path), args);
        }




        //
        // convenience
        //

        public HomePageRepr HomePage()
        {
            return Execute<HomePageRepr>(HttpMethod.Get, RORequestTo(Resource.HomePage));
        }
        public UserRepr User()
        {
            return Execute<UserRepr>(HttpMethod.Get, RORequestTo(Resource.User));
        }
        public ListRepr Services()
        {
            return Execute<ListRepr>(HttpMethod.Get, RORequestTo(Resource.Services));
        }
        public VersionRepr Version()
        {
            return Execute<VersionRepr>(HttpMethod.Get, RORequestTo(Resource.Version));
        }


        public ObjectRepr DomainObject(string domainType, string instanceId)
        {
            return DomainObject<ObjectRepr>(domainType, instanceId);
        }
        public T DomainObject<T>(string domainType, string instanceId) where T : ObjectRepr, new()
        {
            return Execute<T>(HttpMethod.Get, RORequestTo(Resource.DomainObject, domainType, instanceId));
        }

        public ObjectRepr DomainService(string serviceName)
        {
            return DomainService<ObjectRepr>(serviceName);
        }
        public T DomainService<T>(string serviceName) where T:ObjectRepr, new()
        {
            return Execute<T>(HttpMethod.Get, RORequestTo(Resource.DomainService, serviceName));
        }

        public PropertyRepr Property(string domainType, string instanceId, string propertyName)
        {
            return Property<PropertyRepr>(domainType, instanceId, propertyName);
        }
        public T Property<T>(string domainType, string instanceId, string propertyName) where T : PropertyRepr, new()
        {
            return Execute<T>(HttpMethod.Get, RORequestTo(Resource.Property, domainType, instanceId, propertyName));
        }


        public CollectionRepr Collection(string domainType, string instanceId, string collectionName)
        {
            return Collection<CollectionRepr>(domainType, instanceId, collectionName);
        }
        public T Collection<T>(string domainType, string instanceId, string collectionName) where T : CollectionRepr, new()
        {
            return Execute<T>(HttpMethod.Get, RORequestTo(Resource.Collection, domainType, instanceId, collectionName));
        }


        public ActionRepr DomainObjectAction(string domainType, string instanceId, string actionName)
        {
            return DomainObjectAction<ActionRepr>(domainType, instanceId, actionName);
        }
        public T DomainObjectAction<T>(string domainType, string instanceId, string actionName) where T : ActionRepr, new()
        {
            return Execute<T>(HttpMethod.Get, RORequestTo(Resource.DomainObjectAction, domainType, instanceId, actionName));
        }


        public ActionRepr DomainServiceAction(string serviceId, string actionName)
        {
            return DomainServiceAction<ActionRepr>(serviceId, actionName);
        }
        public T DomainServiceAction<T>(string serviceId, string actionName) where T:ActionRepr, new()
        {
            return Execute<T>(HttpMethod.Get, RORequestTo(Resource.DomainServiceAction, serviceId, actionName));
        }



        public DomainTypesRepr DomainTypes()
        {
            return Execute<DomainTypesRepr>(HttpMethod.Get, RORequestTo(Resource.DomainTypes));
        }
        public DomainTypeRepr DomainType(string domainType)
        {
            return Execute<DomainTypeRepr>(HttpMethod.Get, RORequestTo(Resource.DomainType, domainType));
        }
        public DomainTypePropertyRepr DomainTypeProperty(string domainType, string propertyName)
        {
            return Execute<DomainTypePropertyRepr>(HttpMethod.Get, RORequestTo(Resource.DomainTypeProperty, domainType, propertyName));
        }
        public DomainTypeCollectionRepr DomainTypeCollection(string domainType, string collectionName)
        {
            return Execute<DomainTypeCollectionRepr>(HttpMethod.Get, RORequestTo(Resource.DomainTypeCollection, domainType, collectionName));
        }
        public DomainTypeActionRepr DomainTypeAction(string domainType, string actionName)
        {
            return Execute<DomainTypeActionRepr>(HttpMethod.Get, RORequestTo(Resource.DomainTypeAction, domainType, actionName));
        }
        public DomainTypeActionParamRepr DomainTypeActionParam(string domainType, string actionName, string paramName)
        {
            return Execute<DomainTypeActionParamRepr>(HttpMethod.Get, RORequestTo(Resource.DomainTypeActionParam, domainType, actionName, paramName));
        }
    }
    


}
