using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestfulObjects.Applib
{
    public abstract class JsonRepr
    {

        public static T FromString<T>(string str) 
        {
            return FromReader<T>(new StringReader(str));
        }

        public static GenericRepr FromResource(byte[] resourceBytes)
        {
            return FromResource(resourceBytes, Encoding.UTF8);
        }

        public static GenericRepr FromResource(byte[] resourceBytes, Encoding encoding)
        {
            return FromResource<GenericRepr>(resourceBytes, encoding);
        }

        public static T FromResource<T>(byte[] resourceBytes)
        {
            return FromResource<T>(resourceBytes, Encoding.UTF8);
        }

        public static T FromResource<T>(byte[] resourceBytes, Encoding encoding) 
        {
            var reader = new StreamReader(new MemoryStream(resourceBytes), encoding);
            return FromReader<T>(new JsonTextReader(reader));
        }

        public static T FromReader<T>(TextReader reader)
        {
            return FromReader<T>(new JsonTextReader(reader));
        }

        private static T FromReader<T>(JsonReader reader)
        {
            if (typeof(T) == typeof(GenericRepr))
            {
                object genericRepr = new GenericRepr {Token = JToken.ReadFrom(reader)};
                return (T)genericRepr;
            }

            if (typeof(T) == typeof(List<GenericRepr>))
            {

                var array = JToken.ReadFrom(reader) as JArray;
                if (array != null)
                {
                    var jEnumerable = array.Values<JToken>();
                    var list = jEnumerable.Select(token => new GenericRepr {Token = token});
                    return AsT<T>(list);
                }
            }

            return new RestfulSerializer().Deserialize<T>(reader);
        }

        private static T AsT<T>(object list)
        {
            return (T) list;
        }

        public string AsJson()
        {
            try
            {
                var sw = new StringWriter();
                new RestfulSerializer().Serialize(new JsonTextWriter(sw) { Indentation = 2, Formatting = Formatting.Indented }, this);
                return sw.ToString();
            }
            catch
            {
                return ToString();
            }
        }
    }
}
