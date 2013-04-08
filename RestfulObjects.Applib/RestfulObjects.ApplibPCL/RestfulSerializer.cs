using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestfulObjects.Applib
{
    public sealed class RestfulSerializer : JsonSerializer
    {
        public RestfulSerializer()
        {
            this.Converters.Add(new GenericReprJsonConverter());
            this.NullValueHandling = NullValueHandling.Ignore;
        }
    }

    internal class GenericReprJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var genericRepr = value as GenericRepr;
            if (genericRepr != null)
            {
                serializer.Serialize(writer, genericRepr.Token);
                return;
            }
            
            var scalarRepr = value as ScalarRepr;
            if (scalarRepr != null)
            {
                serializer.Serialize(writer, scalarRepr.Value);
                return;
            }
           
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            if (objectType == typeof (GenericRepr))
            {
                return new GenericRepr {Token = token};
            }
            if (objectType == typeof (List<GenericRepr>) && token is JArray)
            {
                var array = (JArray) token;
                return array.Select(t => new GenericRepr {Token = t}).ToList();
            }

            if (objectType == typeof (ScalarRepr) && token is JValue)
            {
                return new ScalarRepr {Value = (JValue) token};
            }

            if (objectType == typeof (List<ScalarRepr>) && token is JArray)
            {
                var array = (JArray) token;
                    return array.Select(t => t is JValue? new ScalarRepr{Value = (JValue)t}: null).ToList();
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof (List<GenericRepr>)) || objectType == typeof (GenericRepr)
                 || (objectType == typeof(List<ScalarRepr>)) || objectType == typeof(ScalarRepr);
        }
    }

}
