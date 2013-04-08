using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RestfulObjects.Applib
{

    public class ScalarRepr : GenericRepr
    {
        public ScalarRepr()
        {
        }
        public ScalarRepr(JValue value) 
        {
            Token = value;
        }

        internal JValue Value { get { return (JValue)this.Token; } set { this.Token = value; } }

        public string AsString()
        {
            return Value.Value as string;
        }
        public long? AsLong()
        {
            return Value.Value as long?;
        }
        public long? AsULong()
        {
            return Value.Value as long?;
        }
        public bool? AsBoolean()
        {
            return Value.Value as bool?;
        }
        public char? AsChar()
        {
            return Value.Value as char?;
        }
        public DateTime? AsDateTime()
        {
            return Value.Value as DateTime?;
        }
        public double? AsDouble()
        {
            return Value.Value as double?;
        }
        public float? AsFloat()
        {
            return Value.Value as float?;
        }
        public Guid? AsGuid()
        {
            return Value.Value as Guid?;
        }
        public object AsObject()
        {
            return Value.Value;
        }
        public TimeSpan? AsTimeSpan()
        {
            return Value.Value as TimeSpan?;
        }
        public Uri AsUri()
        {
            return Value.Value as Uri;
        }
    }
}
