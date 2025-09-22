# Copilot Instructions for StoreApp

## Project Overview
- **StoreApp** is a multi-project .NET solution for an e-commerce platform, organized by domain boundaries:
  - `Entities/`: Domain models and DTOs
  - `Repositories/`: Data access layer (repository pattern)
  - `Services/`: Business logic layer (service pattern)
  - `WebApp/`: ASP.NET Core MVC frontend (controllers, views, components)

## Architecture & Patterns
- **Separation of Concerns:**
  - Entities, repositories, and services are in separate projects for modularity and testability.
  - DTOs are used for data transfer between layers, especially for insertion/update scenarios.
- **Repository Pattern:**
  - All data access is abstracted via interfaces in `Repositories/Contracts/` and implemented in `Repositories/`.
  - `RepositoryManager` coordinates repository access.
- **Service Layer:**
  - Business logic is encapsulated in managers (e.g., `ProductManager`) implementing service interfaces in `Services/Contracts/`.
  - `ServiceManager` aggregates all services for DI.
- **MVC WebApp:**
  - Controllers in `WebApp/Controllers/` handle HTTP requests and interact with services.
  - View components (e.g., `CartSummaryViewComponent`) encapsulate reusable UI logic.
  - Views are in `WebApp/Views/`.

## Developer Workflows
- **Build:**
  - Use `dotnet build StoreApp.sln` from the root to build all projects.
- **Run:**
  - Use `dotnet run --project WebApp/StoreApp.csproj` to start the web frontend.
- **Migrations:**
  - Entity Framework migrations are managed in `WebApp/Migrations/`.
- **Debugging:**
  - Set `WebApp` as the startup project in your IDE for debugging.

## Conventions & Practices
- **DTO Naming:**
  - Suffix DTOs for creation/update with `ForInsertion`/`ForUpdate` (e.g., `ProductDtoForInsertion`).
- **Request Parameters:**
  - Use `RequestParameters/` for query/filtering models (e.g., `ProductRequestParameters`).
- **Configuration:**
  - Entity configurations are in `Repositories/Config/`.
- **Extension Methods:**
  - Use `Repositories/Extensions/` for repository query extensions.

## Integration Points
- **Dependency Injection:**
  - All repositories and services are registered for DI in the web app startup.
- **External Libraries:**
  - Entity Framework Core for ORM
  - ASP.NET Core MVC for web

## Examples
- To add a new entity:
  1. Create model in `Entities/Models/`
  2. Add DTOs in `Entities/Dtos/`
  3. Add repository interface/implementation
  4. Add service interface/implementation
  5. Wire up in DI and expose via controller

Refer to existing files for concrete examples (e.g., `Product`, `ProductRepository`, `ProductManager`).

---

*Update this file as project structure or conventions evolve.*
