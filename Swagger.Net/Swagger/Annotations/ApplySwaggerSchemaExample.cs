using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Web.Http.Controllers;

namespace Swagger.Net.Annotations
{
    public class ApplySwaggerSchemaExample : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type, HttpParameterDescriptor parameterDescriptor = null)
        {
            if (schema.type == "object")
            {
                SwaggerSchemaExampleAttribute swaggerSchemaExampleAttribute = type.GetCustomAttribute<SwaggerSchemaExampleAttribute>();

                if (swaggerSchemaExampleAttribute != null)
                {
                    MethodInfo staticMethodGetExample = type.GetMethod(swaggerSchemaExampleAttribute.StaticMethodName);

                    if (staticMethodGetExample != null)
                    {
                        JsonSerializerSettings controllerSerializerSettings = parameterDescriptor.ActionDescriptor.ControllerDescriptor.Configuration.Formatters.JsonFormatter.SerializerSettings;

                        schema.example = FormatJson(staticMethodGetExample.Invoke(null, null), controllerSerializerSettings);
                    }
                }
            }
        }

        private static object FormatJson(object example, JsonSerializerSettings serializerSettings)
        {
            string jsonString = JsonConvert.SerializeObject(example, serializerSettings);

            return JsonConvert.DeserializeObject(jsonString);
        }
    }
}