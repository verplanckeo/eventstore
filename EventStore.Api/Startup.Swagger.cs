using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EventStore.Api
{
    public partial class Startup
    {
        /// <summary>
        /// Load swagger settings
        /// </summary>
        /// <param name="services"></param>
        public void LoadSwagger(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                //This is to solve the Swagger "Can't use schemaId for type ... the same schemaId is already used for type" issue.
                //When using 2 variables with the same name (in this scenario, request in UsersController), it tries to create 
                //a schemaId "$request" for both the register request and authenticate request.
                c.CustomSchemaIds(type => type.ToString());

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EventStore Api", 
                    Version = "v1",
                    Contact = new OpenApiContact{ Email = "olivier@itigai.com" }
                });

                var xmlDocumentation = Path.Combine(AppContext.BaseDirectory, "EventStore.Api.xml");
                c.IncludeXmlComments(xmlDocumentation);
            });
        }
    }
}
