using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace URIMP
{
    public class ContentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IContent).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string type = jsonObject["Type"].Value<string>();
            string id = jsonObject["Id"].Value<string>();
            string name = jsonObject["Name"].Value<string>();

            return ContentFactory.CreateContent(new ContentData { Type = type, Id = id, Name = name });
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IContent content = (IContent)value;
            JObject jsonObject = new JObject
        {
            { "Type", content.GetType().Name },
            { "Id", content.Id },
            { "Name", content.Name }
        };
            jsonObject.WriteTo(writer);
        }
    }
}
