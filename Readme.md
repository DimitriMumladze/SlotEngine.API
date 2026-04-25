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

**Day 2 of the [14-day backend schedule](../Md's/slot-engine-2-week-backend-schedule.md) complete.** `dotnet build SlotEngine.Domain.csproj` → 0 warnings, 0 errors.

- **Solution** — four Clean Architecture projects + five test projects (`Domain.Tests`, `Application.Tests`, `Infrastructure.Tests`, `Api.Tests`, `Simulation`). `SlotEngine.API` has `Program.cs`, `Controllers/`, default `appsettings.json`.
- **Generic foundations** — `BaseEntity` (Id, CreatedAt, UpdatedAt, Status) and `RecordStatus` enum (Active, Inactive, Deleted) in `Domain/Common/`. Generic `IRepository<T>` in `Application/Abstractions/` with Add, Update, GetById, GetAll, and soft-delete. `Repository<T>` EF Core implementation in `Infrastructure/Persistence/Repositories/`. `AppDbContext` scaffolded, registered via `AddInfrastructure` DI extension using SQL Server (`DefaultConnection` from `appsettings.json`). EF Core 8.0.11 + `Microsoft.EntityFrameworkCore.SqlServer` as dependencies.
- **Day 1 — Domain primitives**
  - `Coins` (long-backed, non-negative, arithmetic + comparison operators) and `Bet` (amount + line count, validates amount is multiple of lines) in `Domain/ValueObjects/`.
  - `SymbolType` enum (Normal, Wild, Scatter, Bonus) in `Domain/Enums/`.
  - `Player` entity in `Domain/Entities/` with `Username` and `Balance`, exposing `DebitBet(Bet)` / `CreditWin(Coins)`.
  - `InsufficientBalanceException` and `InvalidBetException` in `Domain/Exceptions/`.
- **Day 2 — Reel math primitives** (all in `Domain/ValueObjects/`)
  - `ReelStrip` — immutable `IReadOnlyList<int>` of symbol ids, wrap-safe `At(index)`.
  - `ReelSet` — non-empty collection of strips, indexer, `ReelCount`.
  - `Payline` — row-per-reel indices, validates non-negative.
  - `PaytableEntry` (readonly record struct) + `Paytable` with nested `symbolId → matchCount → multiplier` lookup; `Multiplier(...)` returns 0 on miss.
- **Known issues / outstanding tech debt**
  - Test projects target `net9.0` while source projects target `net8.0` — bump source TFMs or pin tests to `net8.0` before adding cross-project test code.
  - Solution file references test projects at `tests/` but they live at `SlotEngine.Tests/` — fix sln paths before full solution build.

## Next steps (per [backend schedule](../Md's/slot-engine-2-week-backend-schedule.md))

- **Day 3** — `IRng` abstraction, `ReelSpinner`, `GridBuilder`, pure `SpinEvaluator` + handler tests with `FakeRng`.
- **Day 4** — RTP simulator (10M spins) with `CryptoRng`; tune reel strips to 94–96% RTP, 20–35% hit frequency.
- **Day 5** — `Game` / `GameConfigVersion` / `SpinRecord` / `Transaction` entities, EF configurations, first migration.
- **Day 6** — `IUnitOfWork` + entity-specific repositories, DI wiring.
- **Day 7** — MediatR + FluentValidation + `SpinHandler` + `/api/spin` endpoint (idempotent via `clientSpinId`).
- **Days 8–14** — CORS, JWT auth, admin API (players, versioned config, simulate, audit), rate limiting, exception middleware, healthcheck, smoke test.

For the full plan and the parallel frontend track, see the [Master 2-Week Schedule](../Md's/slot-engine-2-week-schedule.md).
