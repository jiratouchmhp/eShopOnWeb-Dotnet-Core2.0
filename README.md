# eShopOnWeb - Migration Demo: .NET Core 2.0 → .NET 8 + MVC → Blazor

🚀 **GitHub Copilot Agent Migration Demo Project**

This repository demonstrates the powerful migration capabilities of GitHub Copilot, showcasing an automated upgrade path from:
- **.NET Core 2.0** → **.NET 8.0** (LTS)
- **ASP.NET Core MVC** → **Blazor WebAssembly UI**
- **Legacy patterns** → **Modern .NET 8 architecture**

## 🎯 Demo Overview

This project serves as a comprehensive example of how GitHub Copilot can intelligently modernize legacy .NET applications with minimal manual intervention. The migration follows Microsoft's recommended upgrade patterns and modern architectural principles.

### What This Demo Showcases

✅ **Framework Migration**: Seamless upgrade from .NET Core 2.0 to .NET 8.0  
✅ **UI Modernization**: Complete migration from MVC/Razor Pages to Blazor WebAssembly  
✅ **Dependency Updates**: Automatic NuGet package modernization and compatibility fixes  
✅ **Code Modernization**: Latest C# language features and patterns  
✅ **Architecture Improvements**: Clean Architecture with proper separation of concerns  
✅ **Performance Enhancements**: Modern .NET 8 performance optimizations  

### Original Project Context

Based on the Microsoft eShopOnWeb reference application, this sample demonstrates a single-process (monolithic) e-commerce application architecture. The original project was designed to support the [Architecting Modern Web Applications with ASP.NET Core and Azure eBook](https://aka.ms/webappebook).

**Current State**: .NET Core 2.0 with traditional MVC pattern  
**Target State**: .NET 8.0 with modern Blazor WebAssembly UI

## 🏗️ Architecture Overview

### Current Architecture (.NET Core 2.0)
```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Web (MVC)     │───▶│ ApplicationCore │───▶│ Infrastructure  │
│ Controllers     │    │ Domain Models   │    │ EF Core 2.0     │
│ Views (.cshtml) │    │ Interfaces      │    │ Repositories    │
│ ViewModels      │    │ Services        │    │ Data Access     │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

### Target Architecture (.NET 8.0 + Blazor)
```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│ BlazorAdmin     │    │   Web API       │    │ ApplicationCore │    │ Infrastructure  │
│ (WebAssembly)   │───▶│ Controllers     │───▶│ Domain Models   │───▶│ EF Core 8.0     │
│ Razor Components│    │ Minimal APIs    │    │ Business Logic  │    │ Modern Patterns │
│ .razor files    │    │ Authentication  │    │ Clean Arch      │    │ Performance     │
└─────────────────┘    └─────────────────┘    └─────────────────┘    └─────────────────┘
```

## 🚀 Quick Start Guide

### Prerequisites

Before running this demo application, ensure you have:

- **.NET 8.0 SDK** or later installed ([Download here](https://dotnet.microsoft.com/download/dotnet/8.0))
- **Visual Studio 2022** (17.8+) or **Visual Studio Code** with C# extension
- **SQL Server LocalDB** (optional, app works with in-memory database by default)
- **Git** for version control

### Option 1: Run with In-Memory Database (Recommended for Demo)

This is the fastest way to get the application running for demonstration purposes:

```bash
# Clone the repository
git clone <repository-url>
cd eShopOnWeb-Dotnet-Core2.0

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the application
cd src/Web
dotnet run
```

🌐 **Application will be available at**: `https://localhost:5001` or `http://localhost:5000`

### Option 2: Run with SQL Server Database

For a more persistent data experience:

1. **Update Connection Strings** in `src/Web/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "CatalogConnection": "Server=(localdb)\\mssqllocaldb;Database=eShopOnWeb.CatalogDb;Trusted_Connection=true;MultipleActiveResultSets=true;",
    "IdentityConnection": "Server=(localdb)\\mssqllocaldb;Database=eShopOnWeb.Identity;Trusted_Connection=true;MultipleActiveResultSets=true;"
  }
}
```

2. **Enable SQL Server** in `src/Web/Startup.cs` (uncomment SQL Server configuration)

3. **Run Entity Framework Migrations**:
```bash
cd src/Web

# Create and update Catalog database
dotnet ef database update -c catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj

# Create and update Identity database
dotnet ef database update -c appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj

# Run the application
dotnet run
```

### 🔐 Default Demo Credentials

| Role | Email | Password |
|------|-------|----------|
| Demo User | `demouser@microsoft.com` | `Pass@word1` |
| Administrator | `admin@microsoft.com` | `Pass@word1` |

## 🛠️ Migration Process with GitHub Copilot

### Phase 1: .NET Framework Upgrade
1. **Incremental Upgrades**: .NET Core 2.0 → 3.1 → 6.0 → 8.0
2. **Package Modernization**: Update all NuGet packages to .NET 8 compatible versions
3. **Code Compatibility**: Fix breaking changes and deprecated APIs
4. **Performance Optimizations**: Leverage .NET 8 performance improvements

### Phase 2: MVC to Blazor Migration
1. **Component Migration**: Convert Controllers and Views to Blazor components
2. **State Management**: Implement modern state management patterns
3. **Authentication**: Migrate to Blazor-compatible auth patterns
4. **Interactivity**: Replace jQuery with Blazor event handling
5. **Routing**: Convert MVC routing to Blazor navigation

### Phase 3: Architecture Modernization
1. **Clean Architecture**: Implement proper separation of concerns
2. **Dependency Injection**: Modernize DI patterns for .NET 8
3. **Async Patterns**: Convert synchronous operations to async/await
4. **Performance**: Implement caching, compression, and optimization

## 📋 Migration Checklist Progress

- [ ] **Preparation Phase**
  - [x] Backup and version control setup
  - [x] Dependency audit completed
  - [ ] Test coverage analysis
  - [ ] Browser compatibility check

- [ ] **.NET Framework Upgrades**
  - [ ] Upgrade to .NET Core 3.1
  - [ ] Upgrade to .NET 6.0 (LTS)
  - [ ] Upgrade to .NET 8.0 (LTS)
  - [ ] Package dependency resolution

- [ ] **Code Modernization**
  - [ ] C# language feature updates
  - [ ] Nullable reference types
  - [ ] Async/await patterns
  - [ ] Modern DI patterns

- [ ] **UI Migration (MVC → Blazor)**
  - [ ] Blazor project structure setup
  - [ ] Component migration strategy
  - [ ] Authentication migration
  - [ ] Routing configuration
  - [ ] State management implementation

- [ ] **Validation & Testing**
  - [ ] Unit test migration
  - [ ] Integration test updates
  - [ ] Performance benchmarking
  - [ ] Security validation

## Running the sample

After cloning or downloading the sample you should be able to run it using an In Memory database immediately.

If you wish to use the sample with a persistent database, you will need to run its Entity Framework Core migrations before you will be able to run the app, and update `ConfigureServices` method in `Startup.cs` or `Program.cs` .


### Key Migration Commands

```bash
# Check current .NET version
dotnet --version

# Update project files to target framework
# GitHub Copilot will help automate this process

# Run tests during migration
dotnet test

# Check for security vulnerabilities
dotnet list package --vulnerable

# Analyze code for modernization opportunities
dotnet format --verify-no-changes
```

## 📊 Key Features to Demonstrate

### Before Migration (.NET Core 2.0 + MVC)
- Traditional server-side rendering
- Controller-based architecture
- Razor views with ViewModels
- jQuery for client-side interactions
- Classic ASP.NET Core 2.0 patterns

### After Migration (.NET 8.0 + Blazor)
- **Blazor WebAssembly** for rich client-side UI
- **Component-based architecture** with reusable .razor components
- **Modern C# 12** language features
- **Minimal APIs** for streamlined backend
- **Built-in authentication** with Blazor patterns
- **Performance optimizations** from .NET 8
- **Hot reload** for enhanced development experience

## 🎮 Demo Scenarios

### 1. Framework Upgrade Demo
Show how GitHub Copilot can:
- Automatically update project files
- Resolve package dependencies
- Fix breaking changes
- Modernize code patterns

### 2. UI Migration Demo
Demonstrate migration of:
- **Catalog View** → Blazor Catalog Component
- **Shopping Cart** → Interactive Blazor Cart
- **User Authentication** → Blazor Auth components
- **Product Management** → Admin Blazor components

### 3. Performance Comparison
Compare before and after:
- Application startup time
- Memory usage
- Request/response performance
- Client-side interactivity

## 🚀 Next Steps After Demo

After witnessing the migration demo, consider these follow-up actions:

1. **Implement Additional Features**:
   - Real-time updates with SignalR
   - Progressive Web App (PWA) capabilities
   - Advanced caching strategies
   - Microservices decomposition

2. **Deploy to Cloud**:
   - Azure App Service deployment
   - Container orchestration with Kubernetes
   - CI/CD pipeline automation
   - Azure Static Web Apps for Blazor WASM

3. **Advanced Modernization**:
   - Implement Clean Architecture patterns
   - Add OpenTelemetry observability
   - Integrate with Azure services
   - Implement advanced security features

## 🤝 Contributing

This demo project welcomes contributions! Areas where you can help:

- **Migration Scripts**: Automated migration utilities
- **Documentation**: Improve setup and demo instructions
- **Test Coverage**: Add comprehensive test suites
- **Performance**: Optimize migration patterns
- **Examples**: Additional migration scenarios

## 📚 Learning Resources

- [.NET 8.0 Migration Guide](https://docs.microsoft.com/en-us/dotnet/core/compatibility/)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [Clean Architecture with .NET](https://github.com/jasontaylordev/CleanArchitecture)
- [GitHub Copilot Best Practices](https://docs.github.com/en/copilot)
- [Modern .NET Development Patterns](https://docs.microsoft.com/en-us/dotnet/architecture/)

## 📄 License

This project maintains the same license as the original Microsoft eShopOnWeb sample application.

---

**🚀 Ready to see GitHub Copilot in action? Let's migrate some code!**
