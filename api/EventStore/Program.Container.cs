using Autofac;

namespace EventStore.Console
{
    public static partial class Program
    {
        private static void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<Registrations>();
        }
    }
}
