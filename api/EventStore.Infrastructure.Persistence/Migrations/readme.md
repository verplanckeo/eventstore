To add migrations:
+ Set default project to: EventStore.Infrastructure.Persistence
+ Execute following command: Add-Migration NameOfMigration -StartupProject EventStore.Api -Context RealDbContext

To remove last migration:
+ Set default project to: EventStore.Infrastructure.Persistence
+ Execute following command: Remove-Migration -Context RealDbContext