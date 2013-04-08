using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulObjects.ApplibPCL.Exceptions
{
    public class ROUnderlyingException : ROException
    {
        public ROUnderlyingException(Exception ex)
            : base(ex.Message)
        {
            UnderlyingException = ex;
        }
        public ROUnderlyingException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// underlying exception, if any is available.
        /// </summary>
        public Exception UnderlyingException { get; private set; }
    }
}
