using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace Swagger.Net.Annotations
{
    /// <summary>
    /// Application du SwaggerHeaderParamAttribute
    /// </summary>
    public class ApplySwaggerHeaderParamAttribute : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            operation.parameters = operation.parameters ?? new List<Parameter>();

            var swaggerHeaderParamAttributes = apiDescription.ActionDescriptor.GetCustomAttributes<SwaggerHeaderParamAttribute>().ToList();

            foreach (SwaggerHeaderParamAttribute swaggerHeaderParamAttribute in swaggerHeaderParamAttributes)
            {
                Parameter newParameter = new Parameter
                {
                    name = swaggerHeaderParamAttribute.Name,
                    description = swaggerHeaderParamAttribute.Description,
                    @in = "header",
                    required = swaggerHeaderParamAttribute.Required,
                    type = swaggerHeaderParamAttribute.Type,
                    @default = swaggerHeaderParamAttribute.DefaultValue
                };

                if (swaggerHeaderParamAttribute.Type.StartsWith("array["))
                {
                    newParameter.type = "array";
                    newParameter.items = new PartialSchema()
                    {
                        type = swaggerHeaderParamAttribute.Type.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries)[1]
                    };
                }

                operation.parameters.Add(newParameter);
            }
        }
    }
}