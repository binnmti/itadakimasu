enable-migrations
add-migration InitialCreate

update-database

----
cd Itadakimasu
dotnet ef database drop

dotnet ef migrations add InitialCreate
dotnet ef database update