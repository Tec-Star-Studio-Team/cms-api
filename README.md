# CMS API

Backend API powering the CMS frontend — provides authentication, project management, and content endpoints. Built with .NET 10, ASP.NET Core Minimal APIs, and orchestrated with .NET Aspire.

## Features

- **Authentication** — Register and login with Microsoft Identity, JWT bearer tokens (including a ready-to-use Bearer auth button in the API docs UI).
- **Projects** — Full CRUD (create, get by id, paginated list, edit, delete), authorization-protected.
- **Apps** — Feature scaffolded and in progress (create / get by id).
- **CQRS with Mediator** — Commands and queries dispatched through the [Mediator](https://github.com/martinothamar/Mediator) source-generator library, with a shared pipeline behavior for validation.
- **FluentValidation** — Every command/query has a matching validator, run automatically through the mediation pipeline.
- **Result pattern** — Handlers return a `Result<T>` (success / not found / failure) mapped to consistent HTTP responses instead of throwing for expected failure paths.
- **Global exception handling** — Centralized middleware + `ProblemDetails` for unhandled errors.
- **CORS policies** — Separate Development (localhost React/Vite) and Production (configuration-driven) policies.
- **OpenAPI + Scalar** — Interactive API documentation UI in Development.
- **Observability** — OpenTelemetry tracing/metrics/logging plus health check endpoints, wired through .NET Aspire service defaults.
- **Entity Framework Core + SQL Server** — Code-first persistence with a generic repository/unit-of-work abstraction for Create/Update/Delete, and `AppDbContext` used directly for reads.
- **Automatic migrations** — Pending EF Core migrations are applied automatically on startup in Development.

## Tech Stack

| Layer | Technology |
|---|---|
| Runtime | .NET 10 |
| Web framework | ASP.NET Core Minimal APIs |
| Orchestration | .NET Aspire (`CmsApi.AppHost`) |
| Data access | Entity Framework Core + SQL Server |
| Identity/Auth | Microsoft Identity + JWT Bearer |
| CQRS | Mediator (source generator) |
| Validation | FluentValidation |
| API docs | OpenAPI + Scalar |
| Observability | OpenTelemetry (traces, metrics, logs) + health checks |
| Unit/Integration tests | xUnit, FluentAssertions, Moq, Testcontainers (MsSql) |
| Load tests | NBomber, Bogus |

## Architecture — Feature Folder Structure

Every domain/entity follows the same three-layer shape:

```
CmsApi.Server/
├── Domain/
│   └── Entities/                     # Entities, base entity, repository interfaces
├── Infrastructure/
│   ├── AppDbContext.cs               # EF Core DbContext — used directly for reads
│   └── Persistence/Migrations/       # EF Core migrations
├── Application/
│   ├── Common/                       # Result<T>, paged result, validation behavior, exceptions
│   └── Features/
│       ├── Auth/
│       │   ├── Commands/{Register,Login}/
│       │   └── DTOs/
│       └── Projects/
│           ├── Commands/{CreateProject,EditProject,DeleteProject}/
│           └── Queries/{GetProjectById,GetPaginatedProjects}/
└── Presentation/
    ├── Endpoints/<feature>/          # Minimal API endpoint groups (IEndpoint)
    └── DependencyInjection.cs        # Registers Mediator, validators, endpoints, CORS
```

Each command/query is a `record` with a matching `Validator`, and each feature's endpoints are grouped under `/api/<feature>` in a class implementing `IEndpoint`, auto-discovered and mapped at startup.

## Solution Layout

| Project | Purpose |
|---|---|
| `CmsApi.AppHost` | .NET Aspire orchestrator — spins up SQL Server + the API together |
| `CmsApi.Server` | The API itself (this is what `CmsApi.AppHost` runs) |
| `CmsApi.Tests` | Unit and integration tests (xUnit) |
| `Cms.LoadTests` | Load/performance tests (NBomber) |

## API Endpoints

### Auth (`/api/auth`) — anonymous

| Method | Route | Description |
|---|---|---|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Authenticate and retrieve a JWT token |

### Projects (`/api/projects`) — requires authorization unless noted

| Method | Route | Description |
|---|---|---|
| POST | `/api/projects` | Create a new project |
| GET | `/api/projects/{id}` | Get a project by id |
| GET | `/api/projects` | Get a paginated list of projects *(anonymous)* |
| PUT | `/api/projects/{id}` | Edit an existing project |
| DELETE | `/api/projects/{id}` | Delete a project |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Docker Desktop (used by Aspire to run the SQL Server container), **or** a local SQL Server / LocalDB instance
- `dotnet-ef` tool if you'll run migrations manually: `dotnet tool install --global dotnet-ef`

### Clone & configure secrets

The API reads its JWT secret and DB connection string from configuration; in Development these are read from `appsettings.Development.json` / user secrets, and in Production from Key Vault. Set your own JWT signing key before running:

```bash
dotnet user-secrets set "JwtSettings:SecretKey" "<your-local-secret>" --project CmsApi.Server
```

If you run the solution through Aspire, the SQL Server password is read from a secret parameter (`cmsdb-password`) — Aspire will prompt you to set it via user secrets on first run.

### Run with .NET Aspire (recommended)

This starts SQL Server (in a container) and the API together, wired up automatically:

```bash
dotnet run --project CmsApi.AppHost
```

The Aspire dashboard link will be printed in the console — use it to see logs, traces, and the API's endpoint URL.

### Run the API standalone

If you already have SQL Server / LocalDB running and configured in `appsettings.Development.json`:

```bash
dotnet run --project CmsApi.Server
```

Pending EF Core migrations are applied automatically on startup while in the Development environment.

### API Documentation

While running in Development:

- Scalar UI: `/scalar/v1` — interactive docs with a built-in "Bearer" auth field for testing protected endpoints with a JWT.
- Raw OpenAPI document: `/openapi/v1.json`

### Health Checks (Development only)

- `/health` — full readiness check
- `/alive` — liveness check only

## Database Migrations

```bash
# Create a new migration
dotnet ef migrations add <MigrationName> --project CmsApi.Server --output-dir Infrastructure/Persistence/Migrations

# Apply migrations
dotnet ef database update --project CmsApi.Server
```

## Configuration Reference

Key settings in `appsettings.json` / `appsettings.Development.json`:

| Key | Purpose |
|---|---|
| `ConnectionStrings:cmsdb` | SQL Server connection string |
| `JwtSettings:Issuer` / `Audience` / `SecretKey` / `ExpirationInMinutes` | JWT token generation/validation |
| `Cors:AllowedOrigins` | Allowed origins for the Production CORS policy (Development is hard-coded to `localhost:3000` / `localhost:5173` for React/Vite) |

## Testing

Unit and integration tests (xUnit + FluentAssertions + Moq; integration tests spin up a real SQL Server via Testcontainers):

```bash
dotnet test CmsApi.Tests
```

Load tests (NBomber, with Bogus-generated fake data), reports are written to `Cms.LoadTests/reports`:

```bash
dotnet run --project Cms.LoadTests
```

## Feature Workflow (AI-assisted development)

This repo follows a documented PRD → Spec → Implementation → Verification workflow for new features, supported by a Claude Code skill/command:

1. **PRD** — `docs/prd/<id>-<feature>.md` (see `docs/prd/TEMPLATE.md`)
2. **Spec** — `docs/specs/<id>-<feature>.md` (see `docs/specs/TEMPLATE.md`)
3. **Implementation** — following the architecture and conventions in `CLAUDE.md`
4. **Verification** — every item in `docs/harness/verification-checklist.md` must pass
5. **Status tracking** — `docs/features-log.md` records each feature's status (`Not Started` → `In Progress` → `Implemented` → `Verified`)

Run `/implement-feature <feature-name>` in Claude Code to drive this workflow end-to-end for a given feature branch.

## Git Workflow

- New features are built on `feature/NNN-<name>` branches cut from the latest `main`.
- Automated tooling may commit and push to the feature branch, but opening and merging the Pull Request into `main` is always a manual step.

## Code Conventions

- All code, comments, identifiers, and user-facing strings are written in English.
- Async/await everywhere; `CancellationToken` is threaded through and checked in heavy workloads.
- Commands/Queries are `record` types, each with a matching FluentValidation validator.
- Every new endpoint requires authorization unless explicitly marked anonymous.

See `CLAUDE.md` for the full set of conventions used when extending this project.
