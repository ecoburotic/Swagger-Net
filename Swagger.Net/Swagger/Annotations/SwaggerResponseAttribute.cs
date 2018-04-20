using System;
using System.Net;

namespace Swagger.Net.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerResponseAttribute : Attribute
    {
        public SwaggerResponseAttribute(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }

        public SwaggerResponseAttribute(HttpStatusCode statusCode, string description = null, Type type = null, string typeName = null, string mediaType = null, object examples = null, Type exampleClassType = null, string exampleMethodeName = null)
            : this(statusCode)
        {
            Description = description;
            Type = type;
            TypeName = typeName;
            MediaType = mediaType;
            Examples = examples;
            ExampleClassType = exampleClassType;
            ExampleMethodeName = exampleMethodeName;
        }

        public SwaggerResponseAttribute(int statusCode)
        {
            StatusCode = statusCode;
        }

        public SwaggerResponseAttribute(int statusCode, string description = null, Type type = null, string typeName = null, string mediaType = null, object examples = null, Type exampleClassType = null, string exampleMethodeName = null)
            : this(statusCode)
        {
            Description = description;
            Type = type;
            TypeName = typeName;
            MediaType = mediaType;
            Examples = examples;
            ExampleClassType = exampleClassType;
            ExampleMethodeName = exampleMethodeName;
        }

        public int StatusCode { get; private set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public string TypeName { get; set; }

        public string MediaType { get; set; }

        public object Examples { get; set; }

        /// <summary> 
        /// Classe contenant l'exemple 
        /// </summary> 
        public Type ExampleClassType { get; set; }

        /// <summary> 
        /// Methode de la classe renvoyant l'exemple 
        /// </summary> 
        public string ExampleMethodeName { get; set; }
    }
}