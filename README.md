<img src="assets/NSS-128x128.png" align="right" />

# Nefarius.Utilities.EnglishExceptions

[![MSBuild](https://github.com/nefarius/Nefarius.Utilities.EnglishExceptions/actions/workflows/msbuild.yml/badge.svg)](https://github.com/nefarius/Nefarius.Utilities.EnglishExceptions/actions/workflows/msbuild.yml) ![Requirements](https://img.shields.io/badge/Requires-.NET%20Standard%202.0-blue.svg) [![Nuget](https://img.shields.io/nuget/v/Nefarius.Utilities.EnglishExceptions)](https://www.nuget.org/packages/Nefarius.Utilities.EnglishExceptions/) [![Nuget](https://img.shields.io/nuget/dt/Nefarius.Utilities.EnglishExceptions)](https://www.nuget.org/packages/Nefarius.Utilities.EnglishExceptions/)

Adjust `.csproj` accordingly, example snippet:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
    <AllowUnsafeBlocks>True</AllowUnsafeBlocks>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <Version>1.0.0</Version>
    <RepositoryUrl>https://github.com/nefarius/Nefarius.NuGet.Template</RepositoryUrl>
    <PackageProjectUrl>https://github.com/nefarius/Nefarius.NuGet.Template</PackageProjectUrl>
    <Description>Summary. README takes priority over this but nice to have.</Description>
  </PropertyGroup>

  <Import Project="$(SolutionDir)CommonProjectProperties.targets" />

</Project>
```

## appveyor.yml

Add this artifact filter to grab symbols and live packages:

```yml
artifacts:
- path: 'bin**\*.nupkg'
- path: 'bin**\*.snupkg'
```
