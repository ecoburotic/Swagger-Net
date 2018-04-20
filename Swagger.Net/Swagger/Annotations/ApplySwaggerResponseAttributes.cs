using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Web.Http.Description;
using System.Reflection;
using Newtonsoft.Json;

namespace Swagger.Net.Annotations
{
    public class ApplySwaggerResponseAttributes : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (apiDescription.GetControllerAndActionAttributes<SwaggerResponseRemoveDefaultsAttribute>().Any())
                operation.responses.Clear();

            var responseAttributes = apiDescription
                .GetControllerAndActionAttributes<SwaggerResponseAttribute>()
                .OrderBy(attr => attr.StatusCode);
            foreach (var attr in responseAttributes)
            {
                var statusCode = attr.StatusCode.ToString();

                operation.responses[statusCode] = new Response
                {
                    description = attr.Description ?? InferDescriptionFrom(statusCode),
                    schema = (attr.Type != null) ? schemaRegistry.GetOrRegister(attr.Type, attr.TypeName) : null
                };
                if (attr.MediaType != null && attr.Examples != null)
                {
                    operation.responses[statusCode].examples = new Dictionary<string, object> { { attr.MediaType, attr.Examples } };
                }
                if (attr.ExampleClassType != null && !String.IsNullOrEmpty(attr.ExampleMethodeName))
                {
                    object exampleClass = Activator.CreateInstance(attr.ExampleClassType);

                    MethodInfo exampleMethodInfo = attr.ExampleClassType.GetMethod(attr.ExampleMethodeName);

                    JsonSerializerSettings controllerSerializerSettings = apiDescription.ActionDescriptor.ControllerDescriptor.Configuration.Formatters.JsonFormatter.SerializerSettings;

                    operation.responses[statusCode].examples = FormatJson(exampleMethodInfo.Invoke(exampleClass, null), controllerSerializerSettings);
                }
            }

            var mediaTypes = responseAttributes
                .Where(x => !string.IsNullOrEmpty(x.MediaType))
                .Select(x => x.MediaType).ToList();
            if (mediaTypes.Count > 0)
            {
                operation.produces = mediaTypes;
            }
        }

        public string InferDescriptionFrom(string statusCode)
        {
            HttpStatusCode enumValue;
            if (Enum.TryParse(statusCode, true, out enumValue))
            {
                return enumValue.ToString();
            }
            return null;
        }

        private static object FormatJson(object example, JsonSerializerSettings serializerSettings)
        {
            string jsonString = JsonConvert.SerializeObject(example, serializerSettings);
            
            // on encapsule les booleen, int... 
            if (example.GetType().IsValueType && !jsonString.StartsWith("\""))
            {
                jsonString = "\"" + jsonString + "\"";
            }

            jsonString = "{ \"application/json\": " + jsonString + " }";

            return JsonConvert.DeserializeObject(jsonString);
        }
    }
}