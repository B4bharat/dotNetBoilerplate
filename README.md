# .NET Core Clean Architecture Boilerplate

A clean architecture boilerplate for .NET Core applications following Domain-Driven Design (DDD) principles and CQRS pattern.

## 📁 Project Structure

The solution follows a clean architecture pattern with the following structure:

    src/
    ├── TodoApp.API                 # API layer - Controllers, Middleware, etc.
    ├── TodoApp.Application         # Application layer - Use cases, DTOs, Interfaces
    │   ├── Commands               # Command handlers (Create, Update, Delete)
    │   ├── Queries                # Query handlers (Read operations)
    │   └── DTOs                   # Data Transfer Objects
    ├── TodoApp.Domain             # Domain layer - Entities, Value Objects, Domain Events
    │   ├── Entities              # Domain entities
    │   ├── Events                # Domain events
    │   └── Interfaces            # Domain interfaces
    └── TodoApp.Infrastructure     # Infrastructure layer - External concerns
        ├── Persistence           # Database context and configurations
        ��── Caching              # Caching implementations and configurations
        └── Services              # External service implementations

    tests/
    ├── TodoApp.UnitTests
    │   ├── Application           # Tests for application layer
    │   │   ├── Commands         # Command handler tests
    │   │   ├── Queries          # Query handler tests
    │   │   └── Validators       # Validator tests
    │   ├── Domain               # Tests for domain layer
    │   │   ├── Entities        # Entity tests
    │   │   └── Services        # Domain service tests
    │   └── TestHelpers          # Test utilities and helpers
    │
    └── TodoApp.IntegrationTests
        ├── API                  # API endpoint tests
        ├── Infrastructure       # Infrastructure layer tests
        └── TestHelpers          # Integration test utilities

## 🏗️ Architecture Overview

This project follows Clean Architecture principles with the following layers:

1. **API Layer**: Handles HTTP requests and responses
2. **Application Layer**: Contains application logic and use cases
3. **Domain Layer**: Contains business logic and domain entities
4. **Infrastructure Layer**: Implements external concerns like databases and caching

The caching layer provides:

- Distributed caching using Redis
- In-memory caching for development
- Cache invalidation strategies
- Configurable cache durations
- Cache-aside pattern implementation

## 🚀 Getting Started

### Prerequisites

- .NET 7.0 SDK or later
- SQL Server (or your preferred database)
- Visual Studio 2022 or VS Code

### Setup Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/dotnet-clean-architecture.git
   cd dotnet-clean-architecture
   ```

2. Update the connection string in `appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=TodoDB;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

3. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   cd src/TodoApp.API
   dotnet run
   ```

The API will be available at `https://localhost:5001`

## 🛠️ Development

### Adding a New Migration

```bash
dotnet ef migrations add MigrationName -p src/TodoApp.Infrastructure -s src/TodoApp.API
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Run specific test project
dotnet test tests/TodoApp.UnitTests
```

### Test Categories

- **Unit Tests**: Testing individual components in isolation
- **Integration Tests**: Testing component interactions
- **API Tests**: Testing HTTP endpoints
- **Repository Tests**: Testing data access layer

### Building the Solution

```bash
dotnet build
```

## 📝 API Documentation

Once the application is running, you can access the Swagger documentation at:
`https://localhost:5001/swagger`

The API provides the following endpoints:

- `GET /api/todos` - Get all todos
- `GET /api/todos/{id}` - Get a specific todo
- `POST /api/todos` - Create a new todo
- `PUT /api/todos/{id}` - Update an existing todo
- `DELETE /api/todos/{id}` - Delete a todo

## 🔧 Configuration

The application can be configured through:

1. `appsettings.json` - Default configuration
2. `appsettings.Development.json` - Development environment settings
3. Environment variables
4. Command line arguments

### Cache Configuration

```json
{
  "CacheSettings": {
    "Provider": "Redis",
    "ConnectionString": "localhost:6379",
    "DefaultExpirationMinutes": 30
  }
}
```

## 🤝 Contributing

1. Fork the repository
2. Create a new branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Commit your changes (`git commit -m 'Add some amazing feature'`)
5. Push to the branch (`git push origin feature/amazing-feature`)
6. Submit a pull request

### Coding Standards

- Follow C# coding conventions
- Write unit tests for new features
- Update documentation as needed
- Use meaningful commit messages

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📮 Support

For support, please open an issue in the GitHub repository or contact the maintainers.
