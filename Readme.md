# SlotEngine.API

Backend API for the SlotEngine slot machine system — game logic, RNG, paytables, balances, and spin history.

## Stack
- ASP.NET Core Web API
- Entity Framework Core
- Clean Architecture (Domain / Application / Infrastructure / API)

## Solution layout
```
SlotEngine.API.sln
├── SlotEngine.Domain/           // Entities, value objects — zero deps
├── SlotEngine.Application/      // Use cases, DTOs, interfaces
├── SlotEngine.Infrastructure/   // EF Core, RNG, external services
├── SlotEngine.API/              // Controllers, DI, Program.cs
└── SlotEngine.Tests/
    ├── SlotEngine.Domain.Tests          // value object invariants
    ├── SlotEngine.Application.Tests     // handlers with fake IRng + in-memory repos
    ├── SlotEngine.Infrastructure.Tests
    ├── SlotEngine.Api.Tests             // WebApplicationFactory integration tests
    └── SlotEngine.Simulation            // 10M-spin RTP harness, runs as a test
```

## Getting started
```bash
dotnet restore
dotnet build
dotnet run --project SlotEngine.API
```

## Current State
- Solution scaffolded with four Clean Architecture projects.
- `SlotEngine.API` has `Program.cs`, `Controllers/`, and default `appsettings.json`.
- **Domain layer:** `BaseEntity` abstract class (Id, CreatedAt, UpdatedAt, Status) and `RecordStatus` enum (Active, Inactive, Deleted) in `Domain/Common/`.
- **Application layer:** Generic `IRepository<T>` interface in `Application/Abstractions/` with Add, Update, GetById, GetAll, and soft-delete operations.
- **Infrastructure layer:** Generic `Repository<T>` EF Core implementation in `Infrastructure/Persistence/Repositories/`. `AppDbContext` scaffolded and registered via `AddInfrastructure` DI extension using SQL Server provider (`DefaultConnection` from `appsettings.json`). EF Core 8.0.11 + `Microsoft.EntityFrameworkCore.SqlServer` added as dependencies.
- No game entities, use cases, authentication, RNG, or paytable logic implemented yet.
- xUnit test projects scaffolded under `tests/` (one per layer, plus `SlotEngine.Simulation` RTP harness). No tests written yet.
- Test projects target `net9.0` while source projects target `net8.0` — bump source TFMs or pin tests to `net8.0` before adding cross-project test code.
- **Known issue:** Solution file references test projects at `tests/` but they live at `SlotEngine.Tests/` — fix sln paths before full solution build.

## Next steps
- Define Domain entities (`Player`, `Game`, `SpinRecord`, `Transaction`) and value objects (`Coins`, `Bet`, `ReelStrip`).
- Add first use case: `Spin` in Application.
- Expose `/api/spin` endpoint in API layer.
