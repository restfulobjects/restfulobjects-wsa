using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulObjects.ApplibPCL.Exceptions
{
    public class ROException : Exception
    {
        public ROException(string message) : base(message)
        {
        }
    }
}
