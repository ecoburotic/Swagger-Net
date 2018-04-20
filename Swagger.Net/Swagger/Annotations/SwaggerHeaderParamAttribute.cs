using System;

namespace Swagger.Net.Annotations
{
    /// <summary>
    /// Permet d'ajout un paramétre d'Header
    /// </summary>
    /// <example>
    /// [SwaggerHeaderParam(name: "businessIds", type: "array[integer]", description: "Liste des IdBuisness", required: true, defaultValue: new int[] { 1, 4 })]
    /// </example>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerHeaderParamAttribute : Attribute
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="name">Nom du parametre</param>
        /// <param name="type">Type</param>
        /// <param name="description">Description</param>
        /// <param name="required">Obligatoire</param>
        /// <param name="defaultValue">Valeur par defaut</param>
        public SwaggerHeaderParamAttribute(string name, string type = "string", string description = "", bool required = true, object defaultValue = null)
        {
            Name = name;
            Type = type;
            Description = description;
            Required = required;
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// Nom du parametre
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Obligatoire
        /// </summary>
        public bool Required { get; private set; }

        /// <summary>
        /// Valeur par defaut
        /// </summary>
        public object DefaultValue { get; private set; }
    }
}