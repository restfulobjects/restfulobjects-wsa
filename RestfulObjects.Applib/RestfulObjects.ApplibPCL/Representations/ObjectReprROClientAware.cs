using System.Linq;

namespace RestfulObjects.Applib
{
    public class ObjectReprROClientAware : ObjectRepr, ROClientAware
    {
        /// <summary>
        /// Convenience method to obtain the value of a property, if any.
        /// </summary>
        protected ScalarRepr PropertyValueOf(string propertyName)
        {
            var memberRepr = Members["propertyName"];
            if (memberRepr == null)
            {
                return null;
            }
            if (memberRepr.Value == null)
            {
                return null;
            }
            return memberRepr.Value.CastTo<ScalarRepr>();
        }


        /// <summary>
        /// Convenience method to obtain the details of a collection.
        /// </summary>
        protected CollectionRepr CollectionReprOf(string collectionName)
        {
            var memberRepr = Members[collectionName];
            if (memberRepr == null)
            {
                return null;
            }
            if (memberRepr.Links == null)
            {
                return null;
            }
            var linkRepr =
                memberRepr.Links.Single(l => l.Rel.Contains("details"));
            return linkRepr.Follow<CollectionRepr>(ROClient);
        }


        /// <summary>
        /// Convenience method, obtains the link to invoke an action.
        /// </summary>
        protected LinkRepr InvokeLinkFor(string actionName)
        {
            var detailsLink = Members[actionName].Links.Single(l => l.Rel.Contains("details"));

            var actionRepr = detailsLink.Follow<ActionRepr>(ROClient);
            return actionRepr.Links.Single(l => l.Rel.Contains("invoke"));
        }


        public ROClient ROClient { get; set; }
    }
}
