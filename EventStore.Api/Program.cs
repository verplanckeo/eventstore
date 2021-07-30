using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EventStore.Api
{
    /// <summary>
    /// Bootstrap class
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// Main method called by framework
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Bootstrap the application
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    builder.RegisterModule<Registrations>();
                });
    }
}
