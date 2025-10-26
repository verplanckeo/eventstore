using Autofac;
using EventStore.Api.Infra;
using EventStore.Application.Features.User.Register;
using EventStore.Application.Mediator;
using EventStore.Infrastructure.Http;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Factories;
using EventStore.Infrastructure.Persistence.Repositories;
using EventStore.Infrastructure.Persistence.Repositories.Project;
using EventStore.Infrastructure.Persistence.Repositories.Ticket;
using EventStore.Infrastructure.Persistence.Repositories.User;
using EventStore.Services;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace EventStore.Api
{
    internal class Registrations : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterMediator(ref builder);

            RegisterInfrastructureHttp(ref builder);

            RegisterInfrastructurePersistence(ref builder);

            RegisterRepositories(ref builder);

            RegisterServices(ref builder);
        }

        private void RegisterMediator(ref ContainerBuilder builder)
        {
            var configuration = MediatRConfigurationBuilder
                .Create(typeof(RegisterUserMediatorCommand).Assembly)
                .WithAllOpenGenericHandlerTypesRegistered()
                .Build();

            builder.RegisterMediatR(configuration);
            
            builder.RegisterType<MediatorFactory>().As<IMediatorFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MediatorScope>().As<IMediatorScope>().InstancePerLifetimeScope();
        }

        private void RegisterInfrastructureHttp(ref ContainerBuilder builder)
        {
            builder.RegisterType<EventStoreHttpContext>()
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }

        private void RegisterInfrastructurePersistence(ref ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionManager>().As<ISqlConnectionManager>().InstancePerLifetimeScope();

            builder.RegisterType<RealDbContext>().As<IDatabaseContext>().InstancePerLifetimeScope();
        }

        private void RegisterRepositories(ref ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().AsImplementedInterfaces();
            builder.RegisterType<ReadUserRepository>().AsImplementedInterfaces();
            builder.RegisterType<TicketRepository>().AsImplementedInterfaces();
            builder.RegisterType<ReadTicketRepository>().AsImplementedInterfaces();
            builder.RegisterType<ProjectRepository>().AsImplementedInterfaces();
            builder.RegisterType<ReadProjectRepository>().AsImplementedInterfaces();
            builder.RegisterType<EventStoreRepository>().AsImplementedInterfaces();
        }

        private void RegisterServices(ref ContainerBuilder builder)
        {
            builder.RegisterType<SecurityService>().AsImplementedInterfaces();
        }
    }
}