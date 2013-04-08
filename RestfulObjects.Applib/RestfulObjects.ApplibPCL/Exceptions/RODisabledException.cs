using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulObjects.ApplibPCL.Exceptions
{
    public class RODisabledException : ROException
    {
        public RODisabledException(string message)
            : base(message)
        {
        }
    }
}
