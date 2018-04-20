using System;

namespace Swagger.Net.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SwaggerSchemaExampleAttribute : Attribute
    {
        public SwaggerSchemaExampleAttribute(string staticMethodName = "GetExample")
        {
            StaticMethodName = staticMethodName;
        }

        public string StaticMethodName { get; private set; }
    }
}