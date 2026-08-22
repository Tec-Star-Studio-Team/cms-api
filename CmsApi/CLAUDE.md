# CLAUDE.md

This file defines the working rules for any Claude-based assistant (Claude
Code, Cowork, etc.) operating on this repository.

## Project Overview

This is main API responsible for the Backend of the CMS Frontend. It provides the
endpoints and data.

## Tech Stack

- C# .NET 10+
- ASPIRE, ASP.NET Core
- Minimal APIs
- Entity Framework Core + MS SQL Server
- Fluent Validations
- Fluent Assertions
- Identity Management with Microsoft Identity Provider
- CQRS with Mediator library
- OpenAPI + Scalar
- Generics repository for Create, Update, Delete
- AppDbContext for gets in general
- Unit Tests with FluentAssertions, Moq, xUnit

## Libraries

Do not add or remove libraries without asking the user for permission, and always explain the reason behind your choice.

## Archictecture: Feature Folder Structure

Every entity/domain lives under `CmsApi.Server/Domain/Entities`. Follows this exact shape (mirror the existing `Projects` feature):

### Infrastructure layer
Manage the data access

- DbContext -> `CmsApi.Server/Infrastructure/AppDbContext.cs`

### Application layer
Business Logic based on features

- Features -> `CmsApi.Server/Application/Features`

### Presentation layer
- Endpoints -> `CmsApi.Server/Presentation/Endpoints/<feature>` 
- DI -> `CmsApi.Server/Presentation/DependencyInject.cs`

## Code Conventions

- All code, comments, identifiers, and user-facing strings/error messages
  are written in **English**, regardless of what language the conversation
  with the developer happens in.
- Use Async/Await
- Pass the CancellationToken and call `cancellationToken.ThrowIfCancellationRequested();` during heavy workload (loops).
- Always use `Record` for the Commands/Request/Queries
- Add a validator for the Commands/Request/Queries based on `docs/specs/*<name>*.md`.
- Every new endpoint requires Authorization

## Feature Workflow

For every new feature, follow this process (see the `implement-feature`
skill under `.claude/skills/` for the full step-by-step):

1. Write or read the PRD in `docs/prd/`.
2. Write or read the Feature Spec in `docs/specs/`.
3. Implement following the architecture and conventions above.
4. Verify against `docs/harness/verification-checklist.md`, then update
   the feature's status in `docs/features-log.md` before considering the
   feature done.

## Git Workflow & Safety Rules

- The `/implement-feature` command may commit and push automatically, but
only to the current feature branch. It must never commit, push, or merge
into `main` on its own — opening and merging the Pull Request stays a
manual step for the developer.

## Working Style

- The developer is studying. Explain the reasoning behind non-trivial decisions, not just the code.
- Do not run, build, or test the project inside your own sandbox unless
  explicitly asked — the developer builds and runs it themselves.
