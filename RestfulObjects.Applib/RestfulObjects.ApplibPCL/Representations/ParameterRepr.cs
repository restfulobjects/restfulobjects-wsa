using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulObjects.Applib
{
    public class ParameterRepr
    {
        public List<GenericRepr> Choices { get; set; }
        public GenericRepr Default { get; set; }

        public List<LinkRepr> Links { get; set; }
        public Dictionary<string, GenericRepr> Extensions { get; set; }
    }
}
