using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulObjects.Applib
{
    public interface ROClientAware
    {
        ROClient ROClient { get; set; }
    }

    public static class ROClientAwareExtensions
    {
        public static T Injecting<T>(this T roClientAware, ROClient roClient) where T: ROClientAware
        {
            roClientAware.ROClient = roClient;
            return roClientAware;
        }
    }
}
