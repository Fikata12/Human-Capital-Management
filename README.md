Human Capital Management
Human Capital Management (HCM) is a modular, open-source platform for managing employees, departments, roles, and organizational structure. Built with .NET 8, SQL Server, and a clean API-first architecture. The solution targets small and mid-sized organizations needing a practical and extensible HR system.

Features
Employee CRUD (Create, Read, Update, Delete)

Departments with manager assignment

Roles and permissions (custom, not using ASP.NET Identity)

Role-based access via JWT tokens

User authentication API

Soft-delete for main entities

Many-to-many users ↔ roles

Initial data seeding (roles, departments, users)

Clear separation: API, service, data, presentation layers

XML export for reports (planned)

Tech Stack
.NET 8 (C#)

ASP.NET Core MVC (Web UI)

ASP.NET Core Web API (Auth and People APIs)

SQL Server (GUIDs as IDs)

AutoMapper

Solution Structure
pgsql
Копиране
HCM.sln
│
├── Api/
│   ├── HCM.Api.Auth/          → Authentication, roles, JWT
│   └── HCM.Api.People/        → CRUD for employees
│
├── Data/
│   ├── HCM.Data/              → DbContext, seeding, config
│   └── HCM.Data.Models/       → Entities: Person, User, Department, Role, UserRole
│
├── Services/
│   ├── HCM.Services/          → Business logic
│   └── HCM.Services.Mapping/  → AutoMapper profiles
│
├── Web/
│   ├── HCM.Web/               → ASP.NET Core MVC UI
│   └── HCM.Web.ViewModels/    → ViewModels for the UI
│
└── HCM.Common/                → Constants, enums, validators
