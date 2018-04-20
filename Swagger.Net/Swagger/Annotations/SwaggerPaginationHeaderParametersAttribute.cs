using System;
using System.Collections.Generic;

namespace Swagger.Net.Annotations
{
    /// <summary>
    /// Attribut permettant d'ajouter la doc swagger pour les Headers de pagination
    /// </summary>
    /// <example>
    /// 
    /// 
    /// /// <summary>
    /// Création asynchrone d'un retour HTTP de pagination
    /// </summary>
    /// <typeparam name="T">Type d'élément contenu par les pages</typeparam>
    /// public class PaginatedContentResult<T> : IHttpActionResult
    /// {
    ///     IList<T> responseContent;
    ///     ApiController apiController;
    ///
    ///     int pageSize;
    ///     int pageNumber;
    ///
    ///     /// <summary>
    ///     /// Constructeur
    ///     /// </summary>
    ///     /// <param name="apiController">Controller à l'origine du retour</param>
    ///     /// <param name="responseContent">Retour de l'appel</param>
    ///     /// <param name="defaultPageNumber">Numéro de page si aucun Header transmis</param>
    ///     /// <param name="defaultPageSize">Taille de page si aucun Header transmis</param>
    ///     public PaginatedContentResult(ApiController apiController, IList<T> responseContent, int defaultPageNumber, int defaultPageSize)
    ///     {
    ///         this.apiController = apiController;
    ///         this.responseContent = responseContent;
    ///
    ///         pageSize = defaultPageSize;
    ///         pageNumber = defaultPageNumber;
    ///     }
    ///
    ///     /// <summary>
    ///     /// Création du retour HTTP
    ///     /// </summary>
    ///     /// <param name="cancellationToken"></param>
    ///     /// <returns></returns>
    ///     public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    ///     {
    ///         int nbElementTotal = 0;
    ///
    ///         if (apiController.Request.Headers.Contains("x-page-size"))
    ///         {
    ///             pageSize = int.Parse(apiController.Request.Headers.GetValues("x-page-size").First());
    ///         }
    ///         if (apiController.Request.Headers.Contains("x-page-number"))
    ///         {
    ///             pageNumber = int.Parse(apiController.Request.Headers.GetValues("x-page-number").First());
    ///         }
    ///
    ///
    ///         nbElementTotal = responseContent.Count();
    ///    
    ///         responseContent = responseContent.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    ///    
    ///    
    ///         HttpResponseMessage response = apiController.Request.CreateResponse(HttpStatusCode.OK, responseContent);
    ///    
    ///         response.RequestMessage = apiController.Request;
    ///    
    ///         response.Headers.Add("x-page-size", pageSize.ToString());
    ///         response.Headers.Add("x-page-number", pageNumber.ToString());
    ///    
    ///         response.Headers.Add("x-total-record-count", nbElementTotal.ToString());
    ///         response.Headers.Add("x-number-of-page", Math.Ceiling((decimal) nbElementTotal / (decimal) pageSize).ToString());
    ///    
    ///         return Task.FromResult(response);
    ///     }
    /// }
    /// 
    /// 
    /// </example>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerPaginationHeaderParametersAttribute : Attribute
    {
        public int DefaultPageSize { get; set; } = 10;
        public int DefaultPageNumber { get; set; } = 1;

        /// <summary>
        /// Liste des SwaggerHeaderParamAttribute décrivant les Headers de pagination
        /// </summary>
        public SwaggerHeaderParamAttribute[] RequestHeaderParameters
        {
            get
            {
                return new SwaggerHeaderParamAttribute[]
                {
                    new SwaggerHeaderParamAttribute("x-page-size", "integer", "Taille de page", false, DefaultPageSize),
                    new SwaggerHeaderParamAttribute("x-page-number", "integer", "Numéro de page", false, DefaultPageNumber)
                };
            }
        }

        /// <summary>
        /// Liste des SwaggerHeaderParamAttribute décrivant les Headers de pagination
        /// </summary>
        public List<KeyValuePair<string, Header>> ResponseHeaders
        {
            get
            {
                return new List<KeyValuePair<string, Header>>()
                {
                    new KeyValuePair<string, Header>("x-page-size", new Header(){ description = "Taille de page", type = "integer" }),
                    new KeyValuePair<string, Header>("x-page-number", new Header(){ description = "Numéro de page", type = "integer" }),
                    new KeyValuePair<string, Header>("x-total-record-count", new Header(){ description = "Nombre total d'éléments" , type = "integer"}),
                    new KeyValuePair<string, Header>("x-number-of-page", new Header(){ description = "Nombre de page" , type = "integer"}),
                };
            }
        }
    }
}