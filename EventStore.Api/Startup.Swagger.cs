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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventStore Api", Version = "v1" });
            });
        }
    }
}
