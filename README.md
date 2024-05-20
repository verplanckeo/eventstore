# eventstore
A project to implement an Event source driven api. This project works together with an Angular project I'm developing to learn more about Frontend (see app folder).
The idea is to let it grow over time, like an actual project. But also as a resource for topics I already researched in the past.

Should you have any questions or you wish to discuss certain topics, don't hesitate to contact me: olivier@itigai.com

# How to run - Api
+ Make sure you have an MSSQL server running with a database, EventStoreDb
+ Run EventStore.Console project (this project will execute the required db migrations)
+ Next, run EventStore.Api project (with Visual Studio, set as start up project and press F5)
+ Browse to: http://localhost:4000/swagger to see the available api calls
+ Extra: If you run against an Azure SQL Database - you'll need to perform the following scripts (as admin) on the database. 
 + Create an app registration, in my case I named it eventstore-dev. You need to re-use the name in the SQL script
```
CREATE USER [eventstore-dev] FROM EXTERNAL PROVIDER;
ALTER ROLE [db_datareader] ADD MEMBER [eventstore-dev];
ALTER ROLE [db_datawriter] ADD MEMBER [eventstore-dev];
ALTER ROLE [db_accessadmin] ADD MEMBER [eventstore-dev];
GRANT ALTER ON SCHEMA::dbo TO [eventstore-dev];
GRANT CREATE TABLE TO [eventstore-dev];
```

## To add migrations:
+ Open console in Visual Studio (NuGet package manager console)
+ Set default project to: EventStore.Infrastructure.Persistence
+ Execute following command: Add-Migration NameOfMigration -StartupProject EventStore.Api -Context RealDbContext

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
