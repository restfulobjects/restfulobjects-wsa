namespace RestfulObjects.Spec
{
    /// <summary>
    /// 
    /// </summary>
    public class Rel
    {
        public static readonly Rel DescribedBy = new Rel("describedby");
        public static readonly Rel Help = new Rel("help");
        public static readonly Rel Icon = new Rel("icon");
        public static readonly Rel Previous = new Rel("previous");
        public static readonly Rel Next = new Rel("next");
        public static readonly Rel Self = new Rel("self");
        public static readonly Rel Up = new Rel("up");
        public static readonly Rel Action = new Rel("urn:org.restfulobjects:rels/action");
        public static readonly Rel ActionParam = new Rel("urn:org.restfulobjects:rels/action-param");
        public static readonly Rel AddTo = new Rel("urn:org.restfulobjects:rels/add-to");
        public static readonly Rel Attachment = new Rel("urn:org.restfulobjects:rels/attachment");
        public static readonly Rel Choice = new Rel("urn:org.restfulobjects:rels/choice");
        public static readonly Rel Clear = new Rel("urn:org.restfulobjects:rels/clear");
        public static readonly Rel Collection = new Rel("urn:org.restfulobjects:rels/collection");
        public static readonly Rel Default = new Rel("urn:org.restfulobjects:rels/default");
        public static readonly Rel Delete = new Rel("urn:org.restfulobjects:rels/delete");
        public static readonly Rel Details = new Rel("urn:org.restfulobjects:rels/details");
        public static readonly Rel DomainType = new Rel("urn:org.restfulobjects:rels/domain-type");
        public static readonly Rel DomainTypes = new Rel("urn:org.restfulobjects:rels/domain-types");
        public static readonly Rel Element = new Rel("urn:org.restfulobjects:rels/element");
        public static readonly Rel ElementType = new Rel("urn:org.restfulobjects:rels/element-type");
        public static readonly Rel Invoke = new Rel("urn:org.restfulobjects:rels/invoke");
        public static readonly Rel Modify = new Rel("urn:org.restfulobjects:rels/modify");
        public static readonly Rel Persist = new Rel("urn:org.restfulobjects:rels/persist");
        public static readonly Rel Property = new Rel("urn:org.restfulobjects:rels/property");
        public static readonly Rel RemoveFrom = new Rel("urn:org.restfulobjects:rels/remove-from");
        public static readonly Rel ReturnType = new Rel("urn:org.restfulobjects:rels/action");
        public static readonly Rel Service = new Rel("urn:org.restfulobjects:rels/service");
        public static readonly Rel Services = new Rel("urn:org.restfulobjects:rels/services");
        public static readonly Rel Update = new Rel("urn:org.restfulobjects:rels/update");
        public static readonly Rel User = new Rel("urn:org.restfulobjects:rels/user");
        public static readonly Rel Value = new Rel("urn:org.restfulobjects:rels/value");
        public static readonly Rel Version = new Rel("urn:org.restfulobjects:rels/version");

        public string Name { get; private set; }

        private Rel(string value)
        {
            Name = value;
        }


    }
}
