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
            operation.responses.Clear();

            var responseAttributes = apiDescription
                .GetControllerAndActionAttributes<SwaggerResponseAttribute>()
                .OrderBy(attr => attr.StatusCode);

            List<string> statusCodes = responseAttributes.Select(r => r.StatusCode.ToString()).Distinct().ToList();

            foreach (var attr in responseAttributes)
            {
                string statusCode = attr.StatusCode.ToString();

                statusCode += new string(' ', statusCodes.IndexOf(statusCode));

                string keyStatusCode = statusCode;
                while (operation.responses.ContainsKey(keyStatusCode))
                {
                    keyStatusCode = keyStatusCode + " ";
                }

                operation.responses[keyStatusCode] = new Response
                {
                    description = attr.Description ?? InferDescriptionFrom(keyStatusCode),
                    schema = (attr.Type != null) ? schemaRegistry.GetOrRegister(attr.Type, attr.TypeName) : null
                };
                if (attr.MediaType != null && attr.Examples != null)
                {
                    operation.responses[keyStatusCode].examples = new Dictionary<string, object> { { attr.MediaType, attr.Examples } };
                }
                if (attr.ExampleClassType != null && !String.IsNullOrEmpty(attr.ExampleMethodeName))
                {
                    object exampleClass = Activator.CreateInstance(attr.ExampleClassType);

                    MethodInfo exampleMethodInfo = attr.ExampleClassType.GetMethod(attr.ExampleMethodeName);

                    JsonSerializerSettings controllerSerializerSettings = apiDescription.ActionDescriptor.ControllerDescriptor.Configuration.Formatters.JsonFormatter.SerializerSettings;

                    operation.responses[keyStatusCode].examples = FormatJson(exampleMethodInfo.Invoke(exampleClass, null), controllerSerializerSettings);
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