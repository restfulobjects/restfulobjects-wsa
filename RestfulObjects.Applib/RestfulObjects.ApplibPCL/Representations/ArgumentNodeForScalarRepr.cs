using System;
using Newtonsoft.Json.Linq;

namespace RestfulObjects.Applib
{
    public class ArgumentNodeForScalarRepr : GenericRepr
    {
        public ArgumentNodeForScalarRepr() : base()
        {
        }

        public ArgumentNodeForScalarRepr(string value) 
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(long value) 
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(bool value) 
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(DateTime value)
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(double value)
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(float value)
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(Guid value)
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(object value)
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(TimeSpan value)
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(ulong value)
            : this(new JValue(value))
        {
        }
        public ArgumentNodeForScalarRepr(Uri value)
            : this(new JValue(value))
        {
        }

        private ArgumentNodeForScalarRepr(JValue value)
            : base()
        {
            this.value = new ScalarRepr() { Value = new JValue(value) };
        }
    
        public ScalarRepr value { 
            get {
                return new ScalarRepr()
                    {
                        Value = (JValue) this.Token
                    };
            } 
            set
            {
                this.Token = value.Value;
            }
        }

        /// <summary>
        /// Populated by server if invalid request was submitted.
        /// </summary>
        public string invalidReason { get; set; }
    }
}
