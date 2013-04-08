
using System.Collections.Generic;

namespace RestfulObjects.Applib
{
    public class ArgumentNodeForLinkRepr : GenericRepr
    {
        public ArgumentNodeForLinkRepr(LinkRepr linkRepr) : base()
        {
            value = new HrefRepr { href = linkRepr.Href }; ;
        }

        public HrefRepr value { get; set; }

        public class HrefRepr : JsonRepr
        {
            public string href { get; set; }
        }

        /// <summary>
        /// Populated by server if invalid request was submitted.
        /// </summary>
        public string invalidReason { get; set; }

    }
}
