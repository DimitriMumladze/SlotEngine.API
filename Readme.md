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
└── SlotEngine.API/              // Controllers, DI, Program.cs
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
- Domain, Application, and Infrastructure projects created but empty (no entities, use cases, or EF setup yet).
- No database, authentication, RNG, or paytable logic implemented.
- No tests.

## Next steps
- Define Domain entities (`Player`, `Game`, `SpinRecord`, `Transaction`) and value objects (`Coins`, `Bet`, `ReelStrip`).
- Wire up EF Core `DbContext` in Infrastructure.
- Add first use case: `Spin` in Application.
- Expose `/api/spin` endpoint in API layer.
