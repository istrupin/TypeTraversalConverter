using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TypeTraversalConverter.Models;

namespace TypeTraversalConverter
{
    public class TypeTraversalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var canConvert = objectType.IsAssignableFrom(typeof(BaseClass));
            return canConvert;
        }

        public override bool CanWrite { get { return false; } }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var actualType = GetTypeFromToken(token);
            if (actualType == null)
                throw new InvalidOperationException("invalid object");
            if (existingValue == null || existingValue.GetType() != actualType)
            {
                var contract = serializer.ContractResolver.ResolveContract(actualType);
                existingValue = contract.DefaultCreator();
            }
            using (var subReader = token.CreateReader())
            {
                // Using "populate" avoids infinite recursion.
                serializer.Populate(subReader, existingValue);
            }
            return existingValue;
        }

        Type GetTypeFromToken(JToken token)
        {
            var hierarchyString = token["Hierarchy"].ToString();
            var hierarchy = hierarchyString.Split(':').Select(a => (ClassTypeEnum)Enum.Parse(typeof(ClassTypeEnum), a));
            var firstKey = hierarchy.LastOrDefault(h =>
            {
                return MappingConfig.SerializationMap.ContainsKey(h);
            });
            return MappingConfig.GetType(firstKey);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
