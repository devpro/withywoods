# Devpro Withywoods - Shared .NET libraries

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/withywoods-CI?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=12&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=withywoods&metric=alert_status)](https://sonarcloud.io/dashboard?id=withywoods)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=withywoods&metric=coverage)](https://sonarcloud.io/dashboard?id=withywoods)

Whithywoods is a set of small .NET libraries (Standard/Core) to ease .NET development and push best practices (`less (code) is better! #KISS #DRY`).

All this libraries are available on [nuget.org](https://www.nuget.org/). Feel free to report any issue or ask for a change. You can also contribute with Pull Requests on GitHub.

## Getting started

### Common

Details on this [page](./docs/Common.md).

#### Configuration

[![Version](https://img.shields.io/nuget/v/Withywoods.Configuration.svg)](https://www.nuget.org/packages/Withywoods.Configuration/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Configuration.svg)](https://www.nuget.org/packages/Withywoods.Configuration/)

#### Serialization

[![Version](https://img.shields.io/nuget/v/Withywoods.Serialization.svg)](https://www.nuget.org/packages/Withywoods.Serialization/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Serialization.svg)](https://www.nuget.org/packages/Withywoods.Serialization/)

### Data Access Layer (DAL)

#### MongoDB DAL

Details on this [page](./docs/DalMongoDB.md).

[![Version](https://img.shields.io/nuget/v/Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Withywoods.Dal.MongoDb/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Dal.MongoDb.svg)](https://www.nuget.org/packages/Withywoods.Dal.MongoDb/)

### Web

#### Selenium

[![Version](https://img.shields.io/nuget/v/Withywoods.Selenium.svg)](https://www.nuget.org/packages/Withywoods.Selenium/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.Selenium.svg)](https://www.nuget.org/packages/Withywoods.Selenium/)

#### Web application

Details on this [page](./docs/WebApp.md).

[![Version](https://img.shields.io/nuget/v/Withywoods.AspNetCore.svg)](https://www.nuget.org/packages/Withywoods.AspNetCore/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.AspNetCore.svg)](https://www.nuget.org/packages/Withywoods.AspNetCore/)

#### Web testing

[![Version](https://img.shields.io/nuget/v/Withywoods.WebTesting.svg)](https://www.nuget.org/packages/Withywoods.WebTesting/)
[![Downloads](https://img.shields.io/nuget/dt/Withywoods.WebTesting.svg)](https://www.nuget.org/packages/Withywoods.WebTesting/)

## Build & Debug

- .NET Core SDK must be installed ([download](https://dotnet.microsoft.com/download))
  - Check the version from the command line `dotnet --version` (>= 2.2.104)
- Restore packages (NuGet): `dotnet restore`
- Build the solution: `dotnet run`
- Run the tests: `dotnet test`
