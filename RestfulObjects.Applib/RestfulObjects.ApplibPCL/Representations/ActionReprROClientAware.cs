using System.Collections.Generic;
using System.Linq;

namespace RestfulObjects.Applib
{
    public class ActionReprROClientAware : ActionRepr, ROClientAware
    {
        public ROClient ROClient { get; set; }
    }
}
