using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulObjects.ApplibPCL.Exceptions
{
    public class RONotFoundException : ROException
    {
        public RONotFoundException(string message)
            : base(message)
        {
        }
    }
}
