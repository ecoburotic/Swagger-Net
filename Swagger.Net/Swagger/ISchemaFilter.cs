using System;
using System.Web.Http.Controllers;

namespace Swagger.Net
{
    public interface ISchemaFilter
    {
        void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type, HttpParameterDescriptor parameterDescriptor = null);
    }
}