using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace bilisimHR.API.Employees.Providers
{
    /// <summary>
    /// AssignOAuth2SecurityRequirements
    /// </summary>
    public class AssignOAuth2SecurityRequirements : IOperationFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            var isAuthorized = filterPipeline
                                             .Select(filterInfo => filterInfo.Instance)
                                             .Any(filter => filter is IAuthorizationFilter);

            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            if (isAuthorized && !allowAnonymous)
            {
                operation.parameters = operation.parameters ?? new List<Parameter>();
                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    description = "Bearer Token Key",
                    @in = "header",
                    required = true,
                    type = "string",
                    @default = string.Empty
                });
            }
        }
    }
}