using EventStore.Infrastructure.Seedwork;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api
{
    public partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void LoadConfiguration(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(Configuration.GetSection(nameof(ConnectionStrings)));
            services.Configure<Jwt>(Configuration.GetSection(nameof(Jwt)));
        }
    }
}
