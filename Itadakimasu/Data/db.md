enable-migrations
add-migration InitialCreate

update-database

----
cd Itadakimasu
dotnet ef database drop

dotnet ef migrations add InitialCreate
dotnet ef database update


dotnet ef migrations add AddBaseUrl

----
* Sample
dotnet ef migrations add YourMigration
dotnet ef migrations remove
dotnet build
dotnet ef migrations add YourMigration