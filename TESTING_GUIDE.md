# Repository Testing Guide

## Overview

Comprehensive guide for writing unit and integration tests for Moondesk repositories using modern .NET testing practices.

## Test Project Setup

### 1. Create Test Project

```bash
# Create test project
dotnet new xunit -n Moondesk.DataAccess.Tests
cd Moondesk.DataAccess.Tests

# Add required packages
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.Extensions.Logging.Abstractions
dotnet add package FluentAssertions
dotnet add package Moq
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Testcontainers.PostgreSql

# Add project references
dotnet add reference ../Moondesk.DataAccess/Moondesk.DataAccess.csproj
dotnet add reference ../Moondesk.Domain/Moondesk.Domain.csproj
```

### 2. Test Project Structure

```
Moondesk.DataAccess.Tests/
├── Unit/
│   ├── Repositories/
│   │   ├── UserRepositoryTests.cs
│   │   ├── OrganizationRepositoryTests.cs
│   │   └── ReadingRepositoryTests.cs
│   └── Helpers/
│       └── TestDbContextFactory.cs
├── Integration/
│   ├── DatabaseTests.cs
│   └── RepositoryIntegrationTests.cs
└── Fixtures/
    ├── TestData.cs
    └── DatabaseFixture.cs
```

## Unit Testing Pattern

### 1. Basic Repository Test Template

```csharp
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moondesk.DataAccess.Data;
using Moondesk.DataAccess.Repositories;
using Moondesk.Domain.Models;
using Moq;

namespace Moondesk.DataAccess.Tests.Unit.Repositories;

public class UserRepositoryTests : IDisposable
{
    private readonly MoondeskDbContext _context;
    private readonly UserRepository _repository;
    private readonly Mock<ILogger<UserRepository>> _loggerMock;

    public UserRepositoryTests()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<MoondeskDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MoondeskDbContext(options);
        _loggerMock = new Mock<ILogger<UserRepository>>();
        _repository = new UserRepository(_context, _loggerMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsUser()
    {
        // Arrange
        var user = new User
        {
            Id = "test-user-1",
            Username = "testuser",
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User"
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync("test-user-1");

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be("test-user-1");
        result.Username.Should().Be("testuser");
        result.Email.Should().Be("test@example.com");
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Act
        var result = await _repository.GetByIdAsync("non-existent");

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_WithValidUser_CreatesUser()
    {
        // Arrange
        var user = new User
        {
            Id = "new-user",
            Username = "newuser",
            Email = "new@example.com",
            FirstName = "New",
            LastName = "User"
        };

        // Act
        var result = await _repository.CreateAsync(user);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("new-user");

        var savedUser = await _context.Users.FindAsync("new-user");
        savedUser.Should().NotBeNull();
        savedUser!.Username.Should().Be("newuser");
    }

    [Fact]
    public async Task CreateAsync_WithDuplicateEmail_ThrowsException()
    {
        // Arrange
        var existingUser = new User
        {
            Id = "existing",
            Username = "existing",
            Email = "duplicate@example.com",
            FirstName = "Existing",
            LastName = "User"
        };
        
        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        var newUser = new User
        {
            Id = "new",
            Username = "new",
            Email = "duplicate@example.com",
            FirstName = "New",
            LastName = "User"
        };

        // Act & Assert
        await _repository.Invoking(r => r.CreateAsync(newUser))
            .Should().ThrowAsync<DuplicateEmailException>();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

### 2. Testing Validation Logic

```csharp
[Theory]
[InlineData("")]
[InlineData("  ")]
[InlineData(null)]
public async Task GetByIdAsync_WithInvalidId_ThrowsArgumentException(string invalidId)
{
    // Act & Assert
    await _repository.Invoking(r => r.GetByIdAsync(invalidId))
        .Should().ThrowAsync<ArgumentException>()
        .WithMessage("*cannot be null or empty*");
}

[Fact]
public async Task CreateAsync_WithInvalidEmail_ThrowsArgumentException()
{
    // Arrange
    var user = new User
    {
        Id = "test",
        Username = "test",
        Email = "invalid-email",
        FirstName = "Test",
        LastName = "User"
    };

    // Act & Assert
    await _repository.Invoking(r => r.CreateAsync(user))
        .Should().ThrowAsync<ArgumentException>()
        .WithMessage("*email format*");
}
```

### 3. Testing Logging

```csharp
[Fact]
public async Task CreateAsync_LogsInformation()
{
    // Arrange
    var user = new User
    {
        Id = "test",
        Username = "test",
        Email = "test@example.com",
        FirstName = "Test",
        LastName = "User"
    };

    // Act
    await _repository.CreateAsync(user);

    // Assert
    _loggerMock.Verify(
        x => x.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Creating user")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
        Times.Once);
}
```

## Integration Testing

### 1. Database Integration Tests

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moondesk.DataAccess.Data;
using Moondesk.DataAccess.Repositories;
using Testcontainers.PostgreSql;

namespace Moondesk.DataAccess.Tests.Integration;

public class RepositoryIntegrationTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithDatabase("moondesk_test")
        .WithUsername("test")
        .WithPassword("test")
        .Build();

    private MoondeskDbContext _context = null!;
    private UserRepository _userRepository = null!;

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();

        var options = new DbContextOptionsBuilder<MoondeskDbContext>()
            .UseNpgsql(_postgres.GetConnectionString())
            .UseSnakeCaseNamingConvention()
            .Options;

        _context = new MoondeskDbContext(options);
        await _context.Database.MigrateAsync();

        _userRepository = new UserRepository(_context, NullLogger<UserRepository>.Instance);
    }

    [Fact]
    public async Task UserRepository_FullCrudOperations_WorksCorrectly()
    {
        // Create
        var user = new User
        {
            Id = "integration-test",
            Username = "integrationtest",
            Email = "integration@test.com",
            FirstName = "Integration",
            LastName = "Test"
        };

        var created = await _userRepository.CreateAsync(user);
        created.Should().NotBeNull();

        // Read
        var retrieved = await _userRepository.GetByIdAsync("integration-test");
        retrieved.Should().NotBeNull();
        retrieved!.Username.Should().Be("integrationtest");

        // Update
        retrieved.FirstName = "Updated";
        var updated = await _userRepository.UpdateAsync(retrieved);
        updated.FirstName.Should().Be("Updated");

        // Delete
        await _userRepository.DeleteAsync("integration-test");
        var deleted = await _userRepository.GetByIdAsync("integration-test");
        deleted.Should().BeNull();
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _postgres.DisposeAsync();
    }
}
```

### 2. Performance Testing

```csharp
[Fact]
public async Task ReadingRepository_BulkInsert_PerformsWell()
{
    // Arrange
    var readings = Enumerable.Range(1, 10000)
        .Select(i => new ReadingExtended
        {
            OrganizationId = "test-org",
            SensorId = 1,
            Value = i * 1.5,
            Timestamp = DateTime.UtcNow.AddSeconds(-i)
        })
        .ToList();

    var stopwatch = Stopwatch.StartNew();

    // Act
    await _readingRepository.BulkInsertReadingsAsync(readings);

    // Assert
    stopwatch.Stop();
    stopwatch.ElapsedMilliseconds.Should().BeLessThan(5000); // Should complete in under 5 seconds

    var count = await _context.Readings.CountAsync();
    count.Should().Be(10000);
}
```

## Test Data Management

### 1. Test Data Builder Pattern

```csharp
public class UserBuilder
{
    private User _user = new()
    {
        Id = "default-id",
        Username = "defaultuser",
        Email = "default@example.com",
        FirstName = "Default",
        LastName = "User"
    };

    public UserBuilder WithId(string id)
    {
        _user.Id = id;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public UserBuilder WithUsername(string username)
    {
        _user.Username = username;
        return this;
    }

    public User Build() => _user;
}

// Usage in tests
var user = new UserBuilder()
    .WithId("test-123")
    .WithEmail("test@example.com")
    .Build();
```

### 2. Test Fixtures

```csharp
public class DatabaseFixture : IDisposable
{
    public MoondeskDbContext Context { get; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<MoondeskDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new MoondeskDbContext(options);
        SeedTestData();
    }

    private void SeedTestData()
    {
        var users = new[]
        {
            new User { Id = "user1", Username = "user1", Email = "user1@test.com", FirstName = "User", LastName = "One" },
            new User { Id = "user2", Username = "user2", Email = "user2@test.com", FirstName = "User", LastName = "Two" }
        };

        Context.Users.AddRange(users);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
```

## Best Practices

### 1. Test Organization
- **One test class per repository**
- **Group related tests with nested classes**
- **Use descriptive test names**: `MethodName_Scenario_ExpectedResult`

### 2. Test Data
- **Use builders for complex objects**
- **Keep test data minimal and focused**
- **Use realistic but safe test data**

### 3. Assertions
- **Use FluentAssertions for readable tests**
- **Test both positive and negative scenarios**
- **Verify logging when appropriate**

### 4. Performance
- **Use in-memory database for unit tests**
- **Use real database for integration tests**
- **Test bulk operations with realistic data volumes**

### 5. Cleanup
- **Implement IDisposable for test classes**
- **Use unique database names for parallel tests**
- **Clean up test containers properly**

## Running Tests

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test class
dotnet test --filter "ClassName=UserRepositoryTests"

# Run tests with detailed output
dotnet test --logger "console;verbosity=detailed"
```

## Continuous Integration

```yaml
# GitHub Actions example
- name: Run Tests
  run: |
    dotnet test --configuration Release \
                --collect:"XPlat Code Coverage" \
                --results-directory ./coverage

- name: Upload Coverage
  uses: codecov/codecov-action@v3
  with:
    directory: ./coverage
```

This guide provides a solid foundation for testing your repositories with both unit and integration tests, ensuring reliability and maintainability of your data access layer.
