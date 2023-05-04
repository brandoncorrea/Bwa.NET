# Bwa.NET

Reusable Code for the different flavors of .NET

## NuGet Packages

- [Bwa.Core](https://www.nuget.org/packages/Bwa.Core)
- [Bwa.Core.Data](https://www.nuget.org/packages/Bwa.Core.Data)
- [Bwa.Core.TestUtilities](https://www.nuget.org/packages/Bwa.Core.TestUtilities)

## Publishing Libraries

    nuget SetApiKey YOUR_API_KEY
    cd /path/to/project
    dotnet build --configuration Release
    nuget pack -Prop Configuration=Release
    dotnet nuget push MyProject.1.2.3.nupkg --source https://api.nuget.org/v3/index.json
