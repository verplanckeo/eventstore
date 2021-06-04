using System;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.User.Register;
using EventStore.Application.Mediator;
using EventStore.Infrastructure.Persistence.Database;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventStore
{
    public class HostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IMediatorFactory _mediatorFactory;
        private readonly ISqlConnectionManager _sqlConnectionManager;
        private readonly IDatabaseContext _dbContext;

        public HostedService(
            ILogger<HostedService> logger,
            IHostApplicationLifetime appLifetime,
            ISqlConnectionManager sqlConnectionManager,
            IDatabaseContext dbContext,
            IMediatorFactory mediatorFactory
            )
        {
            _logger = logger;
            _mediatorFactory = mediatorFactory;
            _sqlConnectionManager = sqlConnectionManager;
            _dbContext = dbContext;

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"1. {nameof(StartAsync)} has been called.");

            _logger.LogInformation($"Migrating database ...");
            var dbContext = new RealDbContext(_sqlConnectionManager);
            await dbContext.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation($"Migration done ...");


            _logger.LogInformation($"Registering new user ...");

            var scope = _mediatorFactory.CreateScope();
            var result = await scope.SendAsync(RegisterUserMediatorCommand.CreateCommand("", "Olivier", "Verplancke", "demo"), cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"User was registered with Id: {result.Id}");
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"4. {nameof(StopAsync)} has been called.");

            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("2. OnStarted has been called.");
        }

        private void OnStopping()
        {
            _logger.LogInformation("3. OnStopping has been called.");
        }

        private void OnStopped()
        {
            _logger.LogInformation("5. OnStopped has been called.");
        }
    }
}