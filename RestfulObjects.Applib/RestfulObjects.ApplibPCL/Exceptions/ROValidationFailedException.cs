using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulObjects.ApplibPCL.Exceptions
{
    public class ROValidationFailedException : ROException
    {
        public ROValidationFailedException(string message)
            : base(message)
        {
        }
    }
}
