using EventStore.Infrastructure.Infra;
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
            services.Configure<Infrastructure.Infra.Azure>(Configuration.GetSection(nameof(Azure)));
        }
    }
}
