# eventstore
A project to implement an Event source driven api. This project works together with an Angular project I'm developing to learn more about Frontend (see app folder).
The idea is to let it grow over time, like an actual project. But also as a resource for topics I already researched in the past.

Should you have any questions or you wish to discuss certain topics, don't hesitate to contact me: olivier@itigai.com

# How to run - Api
+ Make sure you have an MSSQL server running with a database, EventStoreDb
+ Run EventStore.Console project (this project will execute the required db migrations)
+ Next, run EventStore.Api project (with Visual Studio, set as start up project and press F5)
+ Browse to: http://localhost:4000/swagger to see the available api calls

## To add migrations:
+ Open console in Visual Studio (NuGet package manager console)
+ Set default project to: EventStore.Infrastructure.Persistence
+ Execute following command: Add-Migration NameOfMigration -StartupProject EventStore -Context RealDbContext

## To remove last migration:
+ Set default project to: EventStore.Infrastructure.Persistence
+ Execute following command: Remove-Migration -Context RealDbContext

# How to run - App
+ Run npm install
+ Run npm start
+ Browse to: http://localhost:4200 to see the application

## Run the app without api backend
+ Edit app.module.ts (uncomment FakeBackendProvider)
+ Run npm install
+ Run npm start
+ Browse to: http://localhost:4200 to see the application
