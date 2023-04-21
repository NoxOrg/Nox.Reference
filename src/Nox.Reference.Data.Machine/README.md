In order to create migrations:
- In powershel or similar command tool go to Nox.Reference\src\Nox.Reference.Data.World
- Run the following command:   dotnet ef  migrations add  <MigrationName>  --project ../NoNox.Reference.Data.World/Nox.Reference.Data.World.csproj
- Created migration will appear in Nox.Reference.Data.World\\Mirations folder

To create or update database:
Run command dotnet ef database update --connection "Data Source=..\\..\\data\\noxreferences.db"

noxreferences.db database file will appear in (RootPath)//data