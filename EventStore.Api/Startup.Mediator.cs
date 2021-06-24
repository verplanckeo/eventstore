using EventStore.Api.Seedwork;
using EventStore.Application.Features.User.Register;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api
{
    public partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void LoadMediator(IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatorValidationBehavior<,>));
            AssemblyScanner.FindValidatorsInAssembly(typeof(RegisterUserMediatorCommandValidator).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
        }
    }
}
