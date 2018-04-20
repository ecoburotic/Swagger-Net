using Swagger.Net.Dummy.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace Swagger.Net.Dummy.SwaggerExtensions
{
    public class RecursiveCallSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type, HttpParameterDescriptor parameterDescriptor = null)
        {
            schema.properties = new Dictionary<string, Schema>();
            schema.properties.Add("ExtraProperty", schemaRegistry.GetOrRegister(typeof(Product)));
        }
    }
}