using Autofac;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories;
using EventStore.Application.Repositories.User;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Factories;
using EventStore.Infrastructure.Persistence.Repositories;
using EventStore.Infrastructure.Persistence.Repositories.User;
using EventStore.Seedwork;
using MediatR.Extensions.Autofac.DependencyInjection;

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
            builder.RegisterMediatR(typeof(Program).Assembly);
            builder.RegisterMediatR(typeof(IEventStoreRepository).Assembly);

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