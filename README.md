# PropertyMgmt SaaS

multi-tenant property management platform backend built with **ASP.NET Core 9**, leveraging **Clean Architecture** and **CQRS** patterns.

---

## 🏗️ Architectural Overview

This repository demonstrates enterprise-grade software engineering practices, ensuring strict data isolation, high scalability, and clean separation of concerns.

The solution is structured into four distinct layers following Clean Architecture principles:

- **`PropertyMgmt.Domain`**: The core layer containing enterprise business rules, entities, value objects, and domain exceptions. Completely decoupled from external frameworks.
- **`PropertyMgmt.Application`**: Implements CQRS (Command Query Responsibility Segregation) using **MediatR**. Contains business use cases, fluent validations, and abstract interfaces.
- **`PropertyMgmt.Infrastructure`**: Handles data persistence (EF Core + SQL Server), ASP.NET Core Identity, JWT token generation, tenant resolution, and real-time transport wrappers.
- **`PropertyMgmt.Api`**: The presentation layer acting as a lightweight entry point. Responsible for middleware orchestration, routing, SignalR hubs, and Swagger documentation.

---

## 💎 Key Architectural & Security Wins

- **Robust Multi-Tenancy Isolation**: Implements automated data filtering via EF Core **Global Query Filters** based on the resolved Tenant ID, preventing cross-tenant data leakage seamlessly.
- **Aspect-Oriented Security (Pipeline Behaviors)**: Features centralized cross-cutting validation (e.g., `ChatAccessValidationBehavior`) that implicitly intercepts MediatR requests to verify user-tenant boundaries before reaching the database handler.
- **Clean API Resource Hierarchies**: Adheres to strict RESTful URL constraints (`/api/conversations/{id}/messages`) separating route context from the payload request body.
- **Containerized Environment**: Fully containerized using multi-stage Docker builds and coordinated via Docker Compose for immediate local orchestration (Database + API).

---

## 🛠️ Tech Stack

- **Framework**: .NET 9.0 (ASP.NET Core)
- **Persistence**: Entity Framework Core 9 / SQL Server
- **Identity**: ASP.NET Core Identity + JWT Bearer Tokens
- **Messaging**: MediatR (CQRS Pattern)
- **Real-Time**: ASP.NET Core SignalR
- **Documentation**: Swagger / OpenAPI with custom security schemas

---

## 📂 Solution Structure

````text
├── src/
│   ├── PropertyMgmt.Api/
│   ├── PropertyMgmt.Application/
│   ├── PropertyMgmt.Domain/
│   └── PropertyMgmt.Infrastructure/
└── tests/
    ├── PropertyMgmt.Application.UnitTests/
    └── PropertyMgmt.Infrastructure.IntegrationTests/

---

## 🚀 Getting Started

### Prerequisites

- Install **.NET 9 SDK**
- Install **Docker Desktop** (optional, for containerized local setup)
- Install **SQL Server** or use the provided Docker Compose SQL Server container

### Install and run locally

1. Clone the repository:

   ```bash
   git clone https://github.com/your-org/PropertyMgmtSaas.git
   cd PropertyMgmtSaas
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Update the database connection string in `PropertyMgmt.Api/appsettings.Development.json` if you are not using Docker.

4. Run the API:

   ```bash
   dotnet run --project PropertyMgmt.Api
   ```

5. Open Swagger UI to explore the API:
   - `https://localhost:7149/swagger`
   - `http://localhost:5241/swagger`

---

## 🐳 Docker

This repository includes a multi-stage Dockerfile for the API and a `docker-compose.yml` file to run both the API and SQL Server together.

### What it runs

- `real_estate_db`: SQL Server 2022 container
- `real_estate_api`: ASP.NET Core 9 API container

### Default Docker Compose configuration

- API exposed on `http://localhost:5000`
- SQL Server connection string configured as:
  `Server=database;Database=RealEstateDb;User Id=sa;Password=YourStrong@Pass123;TrustServerCertificate=True;`

### Build and run

From the repository root:

```bash
docker compose up --build
```

Then browse the Swagger UI:

- `http://localhost:5000/swagger`

### Stop and remove containers

```bash
docker compose down
```

### Notes

- The API Dockerfile exposes ports `8080` and `8081` internally.
- The compose service maps host port `5000` to container port `8080`.
- If you use Docker Compose, you do not need to update local `appsettings` for the SQL Server connection string.

---

## 🔧 Testing

Run unit tests:

```bash
dotnet test PropertyMgmt.Application.UnitTests/PropertyMgmt.Application.UnitTests.csproj
```

Run integration tests:

```bash
dotnet test PropertyMgmt.Infrastructure.IntegrationTests/PropertyMgmt.Infrastructure.IntegrationTests.csproj
```

```
````
