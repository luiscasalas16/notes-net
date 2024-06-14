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

## Comandos

Guía en <https://learn.microsoft.com/en-us/ef/core/cli/dotnet>.

```powershell
# version
dotnet ef
# install
dotnet tool install --global dotnet-ef
# update
dotnet tool update --global dotnet-ef
```

```powershell
# generate context and models from database
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef dbcontext scaffold "Server=myServer;Database=myDataBase;User Id=myUsername;Password=myPassword;" Microsoft.EntityFrameworkCore.SqlServer --output-dir "Models"
```

```powershell
# generate database script from context and models
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef dbcontext script --output "Script.sql"
```

## TODO

- <https://code-maze.com/asp-net-core-web-api-with-ef-core-db-first-approach>
- <https://code-maze.com/net-core-web-api-ef-core-code-first>
