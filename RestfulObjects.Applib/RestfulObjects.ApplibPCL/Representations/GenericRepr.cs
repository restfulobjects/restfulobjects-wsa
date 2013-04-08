using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


namespace RestfulObjects.Applib
{
    /// <summary>
    /// The idea of GenericRepr is to allow any JSON to be deserialized, and subsequently downcasted
    /// into specific typesafe representations.
    /// </summary>
    public class GenericRepr : JsonRepr
    {
        public T CastTo<T>() where T : JsonRepr
        {
            var buf = new StringBuilder();
            TextWriter textWriter = new StringWriter(buf);
            var writer = new JsonTextWriter(textWriter);
            new RestfulSerializer().Serialize(writer, (object)Token ?? this);

            var reader = new StringReader(buf.ToString());
            var converted = FromReader<T>(reader);
            return PropagateRoClientIfAware(converted);
        }

        public List<T> CastToList<T>() where T : JsonRepr
        {
            var buf = new StringBuilder();
            TextWriter textWriter = new StringWriter(buf);
            var writer = new JsonTextWriter(textWriter);
            new RestfulSerializer().Serialize(writer, (object)Token ?? this);

            var reader = new StringReader(buf.ToString());
            var converted = FromReader<List<T>>(reader);
            return converted;
        }

        public static GenericRepr From(JsonRepr source)
        {
            var buf = new StringBuilder();
            TextWriter textWriter = new StringWriter(buf);
            var writer = new JsonTextWriter(textWriter);
            new RestfulSerializer().Serialize(writer, source);
            var converted = JsonRepr.FromString<GenericRepr>(buf.ToString());
            return PropagateRoClientIfAware(source, converted);
        }

        private T PropagateRoClientIfAware<T>(T converted) where T : JsonRepr
        {
            return PropagateRoClientIfAware(this, converted);
        }

        private static T PropagateRoClientIfAware<T>(object original, T converted) where T : JsonRepr
        {
            var thisAsRoClientAware = original as ROClientAware;
            if (thisAsRoClientAware != null)
            {
                var convertedAsRoClientAware = converted as ROClientAware;
                if (convertedAsRoClientAware != null)
                {
                    convertedAsRoClientAware.ROClient = thisAsRoClientAware.ROClient;
                }
            }
            return converted;
        }

        internal JToken Token { get; set; }


    }

}
