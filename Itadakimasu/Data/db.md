enable-migrations
add-migration InitialCreate

update-database
    "DefaultConnection": "(localdb)\\mssqllocaldb;Initial Catalog=ItadakimasuWeb;Trusted_Connection=True;MultipleActiveResultSets=true",

----
cd ItadakimasuWeb

dotnet ef migrations add NewUser
dotnet ef migrations add ImageFirst
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

---
* First 
dotnet tool restore