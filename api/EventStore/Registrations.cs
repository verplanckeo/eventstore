using Autofac;
using EventStore.Application.Mediator;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Factories;
using EventStore.Infrastructure.Persistence.Repositories;
using EventStore.Infrastructure.Persistence.Repositories.User;
using EventStore.Seedwork;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace EventStore
{
    internal class Registrations : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterMediator(ref builder);

            RegisterInfrastructurePersistence(ref builder);

            RegisterRepositories(ref builder);

            RegisterServices(ref builder);
        }

        private void RegisterMediator(ref ContainerBuilder builder)
        {
            var configuration = MediatRConfigurationBuilder
                .Create(typeof(Console.Program).Assembly)
                .WithAllOpenGenericHandlerTypesRegistered()
                .Build();

            builder.RegisterMediatR(configuration);
            
            builder.RegisterType<MediatorFactory>().As<IMediatorFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MediatorScope>().As<IMediatorScope>().InstancePerLifetimeScope();
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
            
        }
    }
}