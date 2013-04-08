using System.Collections.Generic;
using System.Linq;
using RestfulObjects.Spec;

namespace RestfulObjects.Applib
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorRepr : JsonRepr
    {

        public string Message { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }

        //Optional:
        public List<string> StackTrace { get; set; }
        public ErrorRepr CausedBy { get; set; }

    }
}
