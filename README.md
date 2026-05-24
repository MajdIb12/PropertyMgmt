# PropertyMgmt SaaS

A multi-tenant property management backend built with ASP.NET Core and clean architecture principles.

## Overview

This solution implements a property management SaaS platform with the following core capabilities:

- Tenant management and multi-tenancy support
- Property listing CRUD operations
- Image upload for listings
- JWT authentication and ASP.NET Core Identity
- OpenAPI / Swagger documentation
- SignalR notifications
- Clean architecture with separate API, Application, Domain, and Infrastructure layers
- MediatR command/query pipeline with validation and transaction behavior

## Project Structure

- `PropertyMgmt.Api/` - ASP.NET Core Web API project and middleware
- `PropertyMgmt.Application/` - Application layer: commands, queries, business logic, MediatR handlers
- `PropertyMgmt.Domain/` - Domain entities, enums, and value objects
- `PropertyMgmt.Infrastructure/` - Data access, identity, tenancy, notification, file storage
- `PropertyMgmt.Application.UnitTests/` - Unit tests for application logic
- `PropertyMgmt.Infrastructure.IntegrationTests/` - Integration tests for infrastructure components

## Key Features

- `ListingsController` for property listing operations
- `TenantController` for tenant registration and management
- `AuthController` for login and token issuance
- Tenant identification middleware using `X-Tenant-Id` headers or subdomain-based routing
- FluentValidation and pipeline behaviors for request validation and logging
- SignalR notifications support via `/hubs/notifications`

## Requirements

- .NET 9 SDK
- SQL Server or compatible database

## Setup

1. Clone the repository

   ```bash
   git clone https://github.com/your-org/PropertyMgmtSaas.git
   cd PropertyMgmtSaas
   ```

2. Update configuration
   - Set the `DefaultConnection` string in `PropertyMgmt.Api/appsettings.json` or `PropertyMgmt.Api/appsettings.Development.json`
   - Configure `JwtSettings` inside `PropertyMgmt.Api/appsettings.json` with `Key`, `Issuer`, `Audience`, and other JWT values

3. Apply database migrations
   ```bash
   dotnet ef database update --project PropertyMgmt.Infrastructure --startup-project PropertyMgmt.Api
   ```

## Run the API

From the solution root:

```bash
dotnet run --project PropertyMgmt.Api
```

Then open Swagger UI at:

```text
https://localhost:5001/swagger
```

## Testing

Run unit tests:

```bash
dotnet test PropertyMgmt.Application.UnitTests/PropertyMgmt.Application.UnitTests.csproj
```

Run integration tests:

```bash
dotnet test PropertyMgmt.Infrastructure.IntegrationTests/PropertyMgmt.Infrastructure.IntegrationTests.csproj
```

## Notes

- `X-Tenant-Id` header is supported for tenant resolution when needed.
- Authentication is configured using JWT bearer tokens.
- `PropertyMgmt.Infrastructure` registers Entity Framework Core, Identity, file service, tenant services, and SignalR.

## Improvements

Possible enhancements for future iterations:

- Add frontend client or admin panel
- Add complete user registration and role management
- Add deployment scripts and CI/CD pipeline
- Harden production security settings for JWT, CORS, and HTTPS
