# notes-net

Este repositorio contiene ejemplos y documentación relacionada con .Net.

- <https://code-maze.com/dotnet-what-is-it-why-should-we-use-it>
- [Visual Studio](./README-vs.md)
- [Visual Studio Code](./README-vsc.md)
- [ASP.NET](./README-aspnet.md)
- [C#](./README-csharp.md)
- [Entity Framework](./README-ef.md)

## Comandos

### .NET

- NET CLI
  - <https://learn.microsoft.com/en-us/dotnet/core/tools>
  - <https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new>
  - <https://code-maze.com/dotnet-project-templates-creation/>

```powershell
#crear proyecto console
dotnet new console --output NetConsole --name XYZ

#crear proyecto worker
dotnet new worker --output NetWorker --name XYZ

#crear empty solution
dotnet new sln --output . --name XYZ

#crear gitignore
dotnet new gitignore
```

### -Net Framework

- MSBuild
  - <https://learn.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-command-line-reference>

```powershell
#restaurar solución
MsBuild.exe -restore -p:RestorePackagesConfig=true /clp:ErrorsOnly ./COL/XYZ.sln
#compilar release
MsBuild.exe /t:Rebuild /p:Configuration=Release /clp:ErrorsOnly ./COL/XYZ.sln
#compilar debug
MsBuild.exe /t:Rebuild /p:Configuration=Debug /clp:ErrorsOnly ./COL/XYZ.sln
```

## TODO

- <https://code-maze.com/dotnet-using-the-cli-to-build-and-run-net-applications>
