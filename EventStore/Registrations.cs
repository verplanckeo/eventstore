using Autofac;
using EventStore.Application.Repositories.User;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Factories;
using EventStore.Infrastructure.Persistence.Repositories;
using EventStore.Services.User;

namespace EventStore
{
    internal class Registrations : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterInfrastructurePersistence(ref builder);

            RegisterRepositories(ref builder);

            RegisterServices(ref builder);

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
            builder.RegisterType<UserService>().AsImplementedInterfaces();
        }
    }
}