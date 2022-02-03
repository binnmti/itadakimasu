enable-migrations
add-migration InitialCreate

update-database

----
cd Itadakimasu

dotnet ef migrations add RefectorFood
dotnet ef migrations add InitialCreate
dotnet ef migrations add AddBaseUrl
dotnet ef database update

----
* Sample
dotnet ef database drop
dotnet ef migrations add YourMigration
dotnet ef migrations remove
dotnet build
dotnet ef migrations add YourMigration