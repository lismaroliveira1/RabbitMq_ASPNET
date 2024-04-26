# Clean Architecture Solution Template

[![Build](https://github.com/lismaroliveira1/RabbitMq_ASPNET/actions/workflows/build.yml/badge.svg)](https://github.com/lismaroliveira1/RabbitMq_ASPNET/actions/workflows/build.yml)
[![CodeQL](https://github.com/lismaroliveira1/RabbitMq_ASPNET/actions/workflows/codeql.yml/badge.svg)](https://github.com/lismaroliveira1/RabbitMq_ASPNET/actions/workflows/codeql.yml)
[![Nuget](https://img.shields.io/nuget/v/Clean.Architecture.Solution.Template?label=NuGet)](https://www.nuget.org/packages/Clean.Architecture.Solution.Template)
[![Nuget](https://img.shields.io/nuget/dt/Clean.Architecture.Solution.Template?label=Downloads)](https://www.nuget.org/packages/Clean.Architecture.Solution.Template)
![Twitter Follow](https://img.shields.io/twitter/follow/jasontaylordev?label=Follow&style=social)

The goal of this project is to provide a straightforward and efficient approach to microservices-based RestFull API development, leveraging the power of Clean Architecture, Custom Message Bus, and ASP.NET Core. Using this template, you can easily create an API with ASP.NET Core while still following Clean Architecture principles. Getting started is easy - just install the **.NET template** (see below for full details).

If you find this project useful, please give it a star. Thanks! ⭐

## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) (latest version)
- [Docker engine](https://docs.docker.com/?_gl=1*rfa4kg*_ga*ODk5MTI2MjY2LjE3MTI5MzMzOTc.*_ga_XJWPQMJYHQ*MTcxNDEzNjQ5OS42LjEuMTcxNDEzNjUwMS41OC4wLjA.) (latest LTS)

Follow the commands to use the API

Starts all containers in the background without forcing build
```bash
make up
```

Stops all services (if running), builds all projects and restarts it.
```bash
make up_build
```

Stop all services
```bash
make down
```

if prefer local running:
```bash
cd src/Web
make bus && dotnet run
```

## Database

The template is configured to use Postgres Server by default.

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

Running database migrations is easy. Ensure you add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/Web`
* `--output-dir Data/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\Web --output-dir Data\Migrations`


## Technologies

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [RabbitMq](https://www.rabbitmq.com)
* [XUnit](https://xunit.net), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)

## Versions
The main branch is now on .NET 6.0. The following previous versions are available:

* [6.0](https://github.com/lismaroliveira1/RabbitMq_ASPNET/tree/net6.0)
* [5.0](https://github.com/lismaroliveira1/RabbitMq_ASPNET/tree/net5.0)
* [3.1](https://github.com/lismaroliveira1/RabbitMq_ASPNET/tree/netcore3.1)

## Learn More

* [Clean Architecture with ASP.NET Core 3.0 (GOTO 2019)](https://youtu.be/dK4Yb6-LxAk)
* [Clean Architecture with .NET Core: Getting Started](https://jasontaylor.dev/clean-architecture-getting-started/)

## Support

If you are having problems, please let me know by [raising a new issue](https://github.com/lismaroliveira1/RabbitMq_ASPNET/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).
