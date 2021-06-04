using EventStore.Api.Seedwork;
using EventStore.Infrastructure.Seedwork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api
{
    public partial class Startup
    {
        public void LoadMediator(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatorValidationBehavior<,>));
        }
    }
}
