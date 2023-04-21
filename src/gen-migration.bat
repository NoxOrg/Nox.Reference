cd .\Nox.Reference.Data.Machine
dotnet ef  migrations add   %1  -s Nox.Reference.Data.Machine.csproj
dotnet ef database update --connection "Data Source=..\\..\\data\\noxreferences.db"
cd ..
cd .\Nox.Reference.Data.World
 
dotnet ef  migrations add   %1  -s Nox.Reference.Data.World.csproj
dotnet ef database update --connection "Data Source=..\\..\\data\\noxreferences.db"

cd ..