﻿using System;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Factories;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EventStore.Seedwork
{
    public class DbContextFactory : IDesignTimeDbContextFactory<RealDbContext>
    {
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