using Autofac;

namespace EventStore
{
    public static partial class Program
    {
        private static void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<Registrations>();
        }
    }
}
