# Human Capital Management

.NET 8 application for HR management with REST APIs, business logic, and optional ASP.NET Core MVC frontend.

## Project Structure

- `Api/`  
  - `HCM.Api.Auth/` — Auth endpoints (login, roles, JWT)  
  - `HCM.Api.People/` — CRUD for people  
- `Data/`  
  - `HCM.Data/` — DbContext, seeding, config  
  - `HCM.Data.Models/` — Entity models  
- `Services/`  
  - `HCM.Services/` — Business logic  
  - `HCM.Services.Mapping/` — AutoMapper profiles  
- `Web/`  
  - `HCM.Web/` — MVC UI (optional)  
  - `HCM.Web.ViewModels/` — ViewModels  
- `Tests/`  
  - `HCM.Services.Tests/` — Service tests  
  - `HCM.Api.Tests/` — API tests  
- `HCM.Common/` — Constants, enums, validation  

## Features

- Custom role-based authorization (no ASP.NET Identity)
- People CRUD, soft delete, restore
- Many-to-many: Users ↔ Roles
- Departments with manager assignment
- Static role seeding
- RESTful APIs, separated logic layers
- Unit/integration tests (xUnit)
- Optional ASP.NET Core MVC UI
