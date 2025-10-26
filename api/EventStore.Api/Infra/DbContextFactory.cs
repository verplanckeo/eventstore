using System;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Factories;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EventStore.Api.Infra
{
    /// <summary>
    /// Db context factory
    /// </summary>
    public class DbContextFactory : IDesignTimeDbContextFactory<RealDbContext>
    {
        /// <summary>
        /// Create a new instance of db context
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public RealDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            return new RealDbContext(new SqlConnectionManager(configuration));
        }
    }
}