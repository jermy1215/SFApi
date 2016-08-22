using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Mvc;

namespace _Json
{
    public static class JsonHelper
    {        
        /// <summary>
        /// Serializes an object into Json
        /// </summary>
        /// <param name="input">object to serialize</param>
        /// <returns>Json as a ContentResult</returns>
        public static ContentResult JsonContent(object input)
        {
            return new ContentResult() { Content = JsonString(input), ContentType = "application/json" };
        }

        /// <summary>
        /// Serializes an object into Json
        /// </summary>
        /// <param name="input">object to serialize</param>
        /// <param name="SerializeProperties">List<string> of properties to serialize. Any properties or object names not included will not be serialized</param>
        /// <returns>Json as a ContentResult</returns>
        public static ContentResult JsonContent(object input, List<string> SerializeProperties)
        {
            return new ContentResult() { Content = JsonString(input, SerializeProperties), ContentType = "application/json" };
        }

        /// <summary>
        /// Serializes an object into Json
        /// </summary>
        /// <param name="input">object to serialize</param>
        /// <returns>Json as a string</returns>
        public static string JsonString(object input, bool indent = true)
        {
            return JsonConvert.SerializeObject(input, (indent ? Formatting.Indented : Formatting.None));
        }

        /// <summary>
        /// Serializes an object into Json
        /// Example: 
        /// </summary>
        /// <param name="input">object to serialize</param>
        /// <param name="SerializeProperties">List<string> of properties to serialize. Any properties or object names not included will not be serialized</param>
        /// <returns>Json as a string</returns>
        public static string JsonString(object input, List<string> SerializeProperties, bool indent = true)
        {
            DynamicContractResolver contractResolver = new DynamicContractResolver(SerializeProperties);
            return JsonConvert.SerializeObject(input, (indent ? Formatting.Indented : Formatting.None),
                    new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = contractResolver });
        }

        /// <summary>
        /// Generically deserializes a Json string into an object
        /// </summary>
        /// <typeparam name="T">type of object to deserialize Json to</typeparam>
        /// <param name="input">Json string to deserialize</param>
        /// <returns>object of specified type</returns>
        public static T JSONDecode<T>(string input)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static T JSONDecode<T>(string input, string serviceName)
        {
            try
            {
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unexpected character encountered while parsing value"))
                    throw new Exception("The " + serviceName + " service is not responding with the expected content-type. This could indicate that the service is down.");
                else
                    throw ex;
            }
        }
    }

    public class DynamicContractResolver : DefaultContractResolver
    {
        private IList<string> _propertiesToSerialize = null;

        public DynamicContractResolver(IList<string> propertiesToSerialize)
        {
            _propertiesToSerialize = propertiesToSerialize;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            return properties.Where(p => _propertiesToSerialize.Contains(p.PropertyName)).ToList();
        }
    }
}
