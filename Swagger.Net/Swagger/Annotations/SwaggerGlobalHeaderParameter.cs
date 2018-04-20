using Swagger.Net.Application;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace Swagger.Net.Annotations
{
    /// <summary> 
    /// Applique un parametre Header sur toutes les méthodes d'un service 
    /// </summary> 
    public class SwaggerGlobalHeaderParameter : IOperationFilter
    {
        /// <summary> 
        /// Description 
        /// </summary> 
        public string Description { get; set; }

        /// <summary> 
        /// Nom du parametre 
        /// </summary> 
        public string Name { get; set; }

        /// <summary> 
        /// Valeur par defaut 
        /// </summary> 
        public string DefaultValue { get; set; }

        /// <summary> 
        /// Valeur obligatoire
        /// </summary> 
        public bool Required { get; set; }

        /// <summary> 
        /// Enregistrement du Header 
        /// </summary> 
        /// <param name="c"></param> 
        public void Apply(SwaggerDocsConfig c)
        {
            c.OperationFilter(() => this);
        }

        /// <summary> 
        /// Application du header 
        /// </summary> 
        /// <param name="operation"></param> 
        /// <param name="schemaRegistry"></param> 
        /// <param name="apiDescription"></param> 
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            operation.parameters = operation.parameters ?? new List<Parameter>();
            operation.parameters.Add(new Parameter
            {
                name = Name,
                description = Description,
                @in = "header",
                required = Required,
                type = "string",
                @default = DefaultValue
            });
        }
    }
}