using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulObjects.ApplibPCL.Exceptions
{
    public class ROApplicationWarningException : ROException
    {
        public ROApplicationWarningException(string message)
            : base(message)
        {
        }
    }
}
