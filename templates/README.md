# Byndyusoft.Template.Api
ASP.NET Core MVC tracing.

## Usages

```shell
Sample
```

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET Core (version 6.0 or higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/download/dotnet-core/6.0).

##Configuration

### ConnectionStrings
Database connection settings.

Example:
```shell
"ConnectionStrings": {
    "Main": "Server=localhost;Port=5432;Database=localhost;User Id=user1;Password=password1;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=1024;"
  }
```

### Logging
Logging settings.

Example:
```shell
"Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
```

### Jaeger
Jaeger connection settings.

Example:
```shell
"Jaeger": {
    "AgentHost": "localhost",
    "AgentPort": 6831
  }
```

## General folders layout

### src

- Api - Web API Applications
- Api.Client - Client for Web API, extension for client registration in consumer
- Api.Contracts - Contracts for Web API and client
- DataAccess - Data access layer
- Domain - Application business logic
- Migrator - Database migrator based on https://github.com/fluentmigrator/fluentmigrator

### tests
- IntegrationTests - Web API Integration Tests
- UnitTests - Unit Tests

## Package development lifecycle

- Implement package logic in `src`
- Add or addapt unit-tests (prefer before and simultaneously with coding) in `tests`
- Add or change the documentation as needed
- Open pull request in the correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)