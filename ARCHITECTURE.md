# Moondesk Architecture

## Project Structure

```
Moondesk/
├── Moondesk.Domain/           # Domain logic (Class Library)
│   ├── Models/               # Domain entities
│   ├── Interfaces/           # Repository & service contracts
│   ├── Enums/               # Domain enumerations
│   ├── Exceptions/          # Domain-specific exceptions
│   └── Units/               # Localized units system
│
├── Moondesk.DataAccess/      # Data access layer (Class Library)
│   ├── Data/                # DbContext and configurations
│   ├── Models/              # Extended models with OrganizationId
│   ├── Repositories/        # Repository implementations
│   ├── Configuration/       # Database and logging setup
│   ├── Extensions/          # TimescaleDB extensions
│   └── Migrations/          # EF Core migrations
│
├── Moondesk.API/             # API endpoints (Class Library)
│   ├── Controllers/         # API controllers
│   ├── DTOs/               # Data transfer objects
│   ├── Middleware/         # Custom middleware
│   └── Services/           # Application services
│
├── Moondesk.Host/            # Application host (ASP.NET Core Web API)
│   ├── Program.cs          # Application entry point
│   ├── appsettings.json    # Configuration
│   └── logs/               # Application logs
│
└── Moondesk.Edge.Simulator/  # IoT edge simulator (Console App)
    └── ...                 # Simulation logic
```

## Architecture Principles

### 1. **Clean Architecture**
- **Domain** contains business logic and rules
- **DataAccess** handles data persistence
- **API** provides HTTP endpoints
- **Host** orchestrates everything

### 2. **Separation of Concerns**
- Each project has a single responsibility
- Dependencies flow inward (Host → API → DataAccess → Domain)
- Domain has no external dependencies

### 3. **Dependency Injection**
- All services registered in Host project
- Repositories and services use DI
- Configuration managed centrally

## Running the Application

### Development
```bash
cd Moondesk.Host
dotnet run --environment=Development
```

### Production
```bash
cd Moondesk.Host
dotnet run --environment=Production
```

## Project Responsibilities

### **Moondesk.Domain**
- Business entities (User, Organization, Asset, Sensor, etc.)
- Business rules and validation
- Repository and service interfaces
- Domain exceptions
- Localized units system

### **Moondesk.DataAccess**
- Entity Framework DbContext
- Repository implementations
- Database migrations
- TimescaleDB optimizations
- Multi-tenant data isolation

### **Moondesk.API**
- REST API controllers
- Request/Response DTOs
- API middleware
- Application services
- Authentication/Authorization

### **Moondesk.Host**
- Application startup and configuration
- Dependency injection setup
- Logging configuration
- Database initialization
- HTTP pipeline configuration

### **Moondesk.Edge.Simulator**
- IoT device simulation
- Data generation for testing
- Protocol implementations
- Edge computing scenarios

## Benefits of This Architecture

### 1. **Testability**
- Each layer can be tested independently
- Easy to mock dependencies
- Clear separation of concerns

### 2. **Maintainability**
- Changes isolated to specific layers
- Clear project boundaries
- Consistent patterns

### 3. **Scalability**
- Host can be scaled independently
- DataAccess optimized for performance
- API can be versioned separately

### 4. **Deployment Flexibility**
- Single deployable unit (Host)
- Easy containerization
- Configuration externalized

## Development Workflow

1. **Domain First**: Define entities and business rules
2. **DataAccess**: Implement repositories and database logic
3. **API**: Create endpoints and DTOs
4. **Host**: Wire everything together
5. **Test**: Each layer independently

This architecture provides a solid foundation for a scalable, maintainable IoT application with proper separation of concerns and clean dependencies.
