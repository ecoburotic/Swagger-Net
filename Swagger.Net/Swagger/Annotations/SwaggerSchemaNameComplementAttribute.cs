using System;

namespace Swagger.Net.Annotations
{
    /// <summary>
    /// Permet de personnaliser la fin du nom d'un schema
    /// </summary>
    /// <example>
    /// In your SwaggerConfig
    /// 
    /// c.SchemaId(t =>
    /// {
    ///     string name = t.Name;
    ///
    ///     foreach (SwaggerNameComplementAttribute swaggerNameComplementAttribute in t.GetCustomAttributes(typeof(SwaggerNameComplementAttribute), false))
    ///     {
    ///         name += " " + swaggerNameComplementAttribute.NameComplement;
    ///     }
    ///
    ///     return name;
    /// });
    /// </example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SwaggerSchemaNameComplementAttribute : Attribute
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="complement"></param>
        public SwaggerSchemaNameComplementAttribute(string complement)
        {
            this.NameComplement = complement;
        }

        /// <summary>
        /// Complement pour le nom de l'objet
        /// </summary>
        public string NameComplement { get; private set; }
    }
}