# notes-net / ef - Entity Framework

## Documentación

- Oficial
  - <https://learn.microsoft.com/en-us/ef/core/>
  - <https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging>
  - <https://learn.microsoft.com/en-us/ef/core/saving/transactions>
- Adicionales
  - <https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx>
  - <https://code-maze.com/efcore-execute-stored-procedures/>
  - <https://www.learnentityframeworkcore.com/>

## Paquetes

- [EFCore.NamingConventions](https://github.com/efcore/EFCore.NamingConventions/blob/main/README.md)

## TODO

- <https://code-maze.com/asp-net-core-web-api-with-ef-core-db-first-approach>
- <https://code-maze.com/net-core-web-api-ef-core-code-first>

```powershell
# scaffold data objects

# Tools -> NuGet Package Manger -> Package Manger Console

dotnet add ".\NCA.Tracks\NCA.Tracks.ApiRestMin\NCA.Tracks.ApiRestMin.csproj" package "Microsoft.EntityFrameworkCore.SqlServer"
dotnet add ".\NCA.Tracks\NCA.Tracks.ApiRestMin\NCA.Tracks.ApiRestMin.csproj" package "Microsoft.EntityFrameworkCore.Design"

dotnet ef dbcontext scaffold "Server=localhost;Database=Chinook;User Id=sa;Password=DEMO123*;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --project ".\NCA.Tracks\NCA.Tracks.Domain\NCA.Tracks.Domain.csproj" --startup-project ".\NCA.Tracks\NCA.Tracks.ApiRestMin\NCA.Tracks.ApiRestMin.csproj" --output-dir ".\Models"
```
