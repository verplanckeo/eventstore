using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventStore.Infrastructure.Persistence.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventStore.Console
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            var task = MainAsync(args);
            task.Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseEnvironment(environment)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true);
                    builder.AddUserSecrets<Registrations>();
                    builder.AddEnvironmentVariables();
                    builder.AddCommandLine(args);

                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<HostedService>();
                    services.AddDbContext<RealDbContext>();
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    ConfigureContainer(builder);
                });

            using var host = hostBuilder.Build();
            await host.RunAsync();
        }
    }
}
