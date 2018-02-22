using bilisimHR.API.Employees.Properties;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace bilisimHR.API.Employees.Providers
{
    /// <summary>
    /// TokenEndpointDocumentFilter
    /// </summary>
    public class TokenEndpointDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiExplorer"></param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            PathItem item = new PathItem();
            item.post = new Operation();
            item.post.tags = new List<string> { "Auth" };
            item.post.consumes = new List<string> { "application/x-www-form-urlencoded" };
            item.parameters = new List<Parameter>();
            Parameter grantType = new Parameter() {
                type = "string",
                name = "grant_type",
                required = true,
                @in = "password"
            };
            Parameter userName = new Parameter()
            {
                type = "string",
                name = "username",
                required = true,
                @in = "Kullanıcı Adı"
            };
            Parameter password = new Parameter()
            {
                type = "string",
                name = "password",
                required = true,
                @in = "Şifre"
            };
            Parameter clientId = new Parameter()
            {
                type = "string",
                name = "client_id",
                required = true,
                @in = "client_id"
            };
            item.parameters.Add(clientId);
            item.parameters.Add(password);
            item.parameters.Add(userName);
            item.parameters.Add(grantType);
            swaggerDoc.paths.Add(Settings.Default.TokenPath, item);

            //swaggerDoc.paths.Add(Settings.Default.TokenPath, new PathItem
            //{

            //    post = new Operation
            //    {
            //        tags = new List<string> { "Auth" },
            //        consumes = new List<string>
            //    {
            //        "application/x-www-form-urlencoded"
            //    },
            //        parameters = new List<Parameter> {
            //        new Parameter
            //        {
            //            type = "string",
            //            name = "grant_type",
            //            required = true,
            //            @in = "password"
            //        },
            //        new Parameter
            //        {
            //            type = "string",
            //            name = "username",
            //            required = true,
            //            @in = "Kullanıcı Adı"
            //        },
            //        new Parameter
            //        {
            //            type = "string",
            //            name = "password",
            //            required = true,
            //            @in = "Şifre"
            //        },
            //        new Parameter
            //        {
            //            type = "string",
            //            name = "client_id",
            //            required = true,
            //            @in = "Client ID"
            //        }
            //    }
            //    }
            //});
        }
    }
}