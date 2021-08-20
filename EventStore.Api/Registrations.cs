using System.Linq;
using Autofac;
using EventStore.Api.Seedwork;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories;
using EventStore.Application.Repositories.User;
using EventStore.Infrastructure.Http;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Factories;
using EventStore.Infrastructure.Persistence.Repositories;
using EventStore.Infrastructure.Persistence.Repositories.User;
using EventStore.Services;
using MediatR.Extensions.Autofac.DependencyInjection;

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
            builder.RegisterMediatR(typeof(Program).Assembly);
            builder.RegisterMediatR(typeof(IEventStoreRepository).Assembly);

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
            builder.RegisterType<EventStoreRepository>().AsImplementedInterfaces();
        }

        private void RegisterServices(ref ContainerBuilder builder)
        {
            builder.RegisterType<SecurityService>().AsImplementedInterfaces();
        }
    }
}