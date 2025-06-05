
Migration Checklist: .NET Core 2.0 to .NET 8.0 + Razor to Blazor

This comprehensive checklist helps guide the upgrade and modernization of your legacy .NET Core 2.0 application to .NET 8 and a full Blazor-based UI.

---

1. Preparation

- [ ] Back up the codebase and ensure version control is up to date.
- [ ] Ensure good test coverage (unit, integration, UI tests).
- [ ] List all dependencies (NuGet packages & third-party libraries).
- [ ] Audit third-party libraries for compatibility and maintenance status.
- [ ] Check for custom build steps or MSBuild tasks that may break during upgrades.
- [ ] Evaluate browser support and front-end dependencies (for WebAssembly if applicable).

---

2. Incremental .NET Upgrade Steps

- [ ] Upgrade to .NET Core 3.1
  - Update Target Framework in `.csproj` to `netcoreapp3.1`.
  - Update all NuGet packages to versions compatible with 3.1.
  - Refactor code for breaking changes (e.g., endpoint routing, API deprecations).
  - Run and fix build/test errors.

- [ ] Upgrade to .NET 6.0 (LTS)
  - Update Target Framework to `net6.0`.
  - Migrate to the minimal hosting model (`Program.cs` and `WebApplication.CreateBuilder`).
  - Refactor obsolete code (e.g., `Startup.cs`, `IWebHostBuilder` patterns).
  - Remove `Startup.cs` completely and consolidate all logic into `Program.cs`.
  - Run and fix build/test errors.

- [ ] Upgrade to .NET 8.0 (LTS)
  - Update Target Framework to `net8.0`.
  - Update NuGet packages to latest versions compatible with .NET 8.
  - Refactor for breaking changes using Microsoft’s official compatibility docs.
  - Use `dotnet workload install upgrade-assistant` for automation.
  - Run and fix build/test errors.

---

3. Project File & Dependency Modernization

- [ ] Convert all projects to SDK-style `.csproj` format (if not already).
- [ ] Remove legacy or unused settings/references.
- [ ] Replace deprecated NuGet packages.
- [ ] Enable Nullable Reference Types.
- [ ] Consolidate package versions using `Directory.Packages.props` (if applicable).
- [ ] Update static assets management (CSS, JS, images).

---

4. Code Modernization & Cleanup

- [ ] Update to latest C# language features (e.g., pattern matching, records, file-scoped namespaces).
- [ ] Remove or refactor obsolete APIs/usages flagged by the compiler.
- [ ] Refactor dependency injection and configuration patterns to modern standards.
- [ ] Replace HttpClient usage with IHttpClientFactory.
- [ ] Convert sync I/O to async where applicable.
- [ ] Evaluate adoption of source generators or interceptors.
- [ ] Use in-memory database (e.g., EF Core InMemoryDatabase) for development and testing only.
- [ ] Configure SQL-based databases (e.g., SQL Server, PostgreSQL) for production only.
  - Use environment-based `appsettings.{Environment}.json` and DI registration.
  - Validate correct DBContext registration in each environment.

---

5. Testing & Build Pipeline Updates

- [ ] Update test projects to target .NET 8.
- [ ] Update CI/CD pipelines to use the .NET 8 SDK.
- [ ] Ensure compatibility of test frameworks (e.g., xUnit, MSTest).
- [ ] Run all unit, integration, and UI tests.
- [ ] Add baseline performance benchmarks (e.g., using BenchmarkDotNet).
- [ ] Ensure pipeline supports workload installations.

---

6. UI Migration: Migrate to Full Blazor UI (Cut Off Razor Pages/MVC)

- [ ] Decide between Blazor Server or Blazor WebAssembly as your new front-end architecture.
- [ ] Plan and enforce complete migration to Blazor UI based on current UI pattern:
  - If project is using **MVC pattern**:
    - Migrate all Controllers, Views, and ViewModels to Blazor components and services.
    - Remove all `Controllers`, `.cshtml` views, and MVC-specific folders (e.g., `Views`, `Controllers`).
  - If project is using **Razor Pages**:
    - Migrate `.cshtml` Razor Pages to `.razor` components.
    - Remove Razor Page folders and related code.
  - In both cases:
    - Ensure the project does not contain any `.cshtml` or MVC controller logic.
    - Validate that only `.razor` components exist in the UI layer.
- [ ] Rebuild UI using Blazor components:
  - Use `@page` directive for routing.
  - Move layout logic (`_Layout.cshtml`) to `MainLayout.razor`.
  - Replace `_ViewImports.cshtml` and partial views with shared components.
- [ ] Move business logic from controllers into services or component classes.
- [ ] Replace JavaScript/jQuery-based interactivity with Blazor’s built-in event handling or JS interop.
- [ ] Convert form validation to use Blazor’s `EditForm`, `DataAnnotationsValidator`, and `ValidationSummary`.
- [ ] Set up navigation and routing using `<NavLink>` and `Router`.
- [ ] Implement authentication/authorization using `CascadingAuthenticationState`, `AuthorizeView`, and `IAuthorizationService`.
- [ ] Thoroughly test each migrated UI component, especially interactive or data-bound sections.
- [ ] Remove obsolete Razor artifacts after validation (views, pages, layout, partials, controller logic).

---

7. Validation & Final Steps

- [ ] Perform manual and automated regression testing.
- [ ] Review and remove unused files, assets, and dependencies.
- [ ] Update internal documentation (README, setup guides, architecture notes).
- [ ] Notify users/stakeholders of any feature deprecations or changes.
- [ ] Create rollback or blue/green deployment strategy.
- [ ] Merge to the main branch and deploy to staging/production.

---

8. Optional Modern Enhancements (Post-Migration)

- [ ] Adopt Rate-Limiting Middleware in .NET 8.
- [ ] Use Output Caching for performance gains.
- [ ] Leverage source generators for DTO mapping.
- [ ] Implement OpenTelemetry for observability and distributed tracing.
- [ ] Upgrade front-end styling with TailwindCSS or Fluent UI.
- [ ] Use TimeProvider API for testable time handling.

---

Good Practices for Modern .NET 8 + Blazor Solutions

1. Separation of Concerns
- Organize your solution into clear, purpose-driven projects:
  - BlazorAdmin: Dedicated Blazor WebAssembly app for admin UI.
  - BlazorShared: Contains models, interfaces, and DTOs shared between the admin and main web projects.
  - ApplicationCore: Business logic, domain models, and interfaces.
  - Infrastructure: Implementation of data access (e.g., EF Core), external APIs, and persistence.
  - Web: The main ASP.NET Core web application (can host Blazor WASM, REST APIs, etc.).

2. Clean Architecture Pattern
- Design your codebase so that dependencies flow inward:
  - Core domain and business logic (ApplicationCore) should not depend on infrastructure or web layers.
  - The web and infrastructure layers depend on core abstractions, not vice versa.
  - This approach promotes testability, maintainability, and flexibility.

3. Blazor WebAssembly Server Integration
- Use the Microsoft.AspNetCore.Components.WebAssembly.Server package to host your Blazor WASM (Admin) app within the main web application.
  - This enables shared authentication, routing, and asset management between your main site and admin dashboard.
  - Ensures a unified deployment and a streamlined developer experience.

Sample Project Structure:
  /src
    /BlazorAdmin        <-- Blazor WASM admin UI
    /BlazorShared       <-- Shared models/interfaces
    /ApplicationCore    <-- Core business/domain logic
    /Infrastructure     <-- Data, APIs, external services
    /Web                <-- Main ASP.NET Core app, can host BlazorAdmin

---

Resources
- Breaking changes in .NET Core/ASP.NET Core: https://learn.microsoft.com/en-us/dotnet/core/compatibility/
- Migrate from Razor Pages to Blazor: https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-8.0
- Blazor Documentation: https://learn.microsoft.com/en-us/aspnet/core/blazor/
- Blazor Security: https://learn.microsoft.com/en-us/aspnet/core/blazor/security/?view=aspnetcore-8.0
- Modern Web App Patterns with Blazor: https://learn.microsoft.com/en-us/dotnet/architecture/blazor-for-web-forms-developers/
