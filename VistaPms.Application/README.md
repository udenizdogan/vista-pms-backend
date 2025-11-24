# VistaPms.Application Layer

## Overview

Application katmanı, VistaPms projesinin business logic ve use case'lerini içerir. Clean Architecture prensiplerine uygun olarak MediatR tabanlı CQRS pattern kullanır.

## Features

- ✅ **MediatR CQRS Pattern** - Commands ve Queries ayrımı
- ✅ **FluentValidation** - Request validation
- ✅ **Mapster** - Object mapping
- ✅ **Pipeline Behaviors** - Validation, Logging, Performance tracking
- ✅ **Ardalis.Result** - Result wrapper pattern
- ✅ **Repository Interfaces** - Infrastructure abstraction

## Structure

```
VistaPms.Application/
├── Common/
│   ├── Behaviors/           # MediatR pipeline behaviors
│   ├── Exceptions/          # Custom exceptions
│   └── Interfaces/          # Repository interfaces
├── DTOs/                    # Data Transfer Objects
│   ├── Rooms/
│   ├── Reservations/
│   └── Guests/
├── Features/                # CQRS handlers
│   ├── Rooms/
│   │   ├── Commands/
│   │   └── Queries/
│   ├── Reservations/
│   └── Guests/
├── Mapping/                 # Mapster configuration
├── Services/                # Service interfaces
└── Extensions/              # DI extensions
```

## Usage

### 1. Register Application Services

In your `Program.cs` or `Startup.cs`:

```csharp
using VistaPms.Application.Extensions;

builder.Services.AddApplicationServices();
```

### 2. Implement Repository Interfaces

In `VistaPms.Infrastructure`, implement the repository interfaces:

- `IRoomRepository`
- `IReservationRepository`
- `IGuestRepository`
- `IUnitOfWork`

### 3. Use MediatR in Controllers

```csharp
[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRoomByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
}
```

## Pipeline Behaviors

### ValidationBehavior
Automatically validates all requests using FluentValidation validators.

### LoggingBehavior
Logs request entry, exit, and execution time.

### PerformanceBehavior
Warns about slow requests (>500ms).

## Dependencies

- **MediatR** - CQRS implementation
- **FluentValidation** - Request validation
- **Mapster** - Object mapping
- **Ardalis.Result** - Result pattern
- **Microsoft.Extensions.Logging.Abstractions** - Logging

## Example: Creating a Room

```csharp
var command = new CreateRoomCommand
{
    Number = "101",
    FloorId = floorId,
    RoomTypeId = roomTypeId,
    Capacity = 2,
    Notes = "Ocean view"
};

var result = await _mediator.Send(command);

if (result.IsSuccess)
{
    var response = result.Value;
    Console.WriteLine($"Room created: {response.Number}");
}
```

## Validation Example

```csharp
public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Room number is required")
            .MaximumLength(20);

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0");
    }
}
```

## Testing

Unit tests are located in `VistaPms.Application.Tests` project.

Example test:

```csharp
[Fact]
public async Task CreateRoom_ValidCommand_ReturnsSuccess()
{
    // Arrange
    var mockRepository = new Mock<IRoomRepository>();
    var mockUnitOfWork = new Mock<IUnitOfWork>();
    var handler = new CreateRoomCommandHandler(mockRepository.Object, mockUnitOfWork.Object);

    var command = new CreateRoomCommand { Number = "101", ... };

    // Act
    var result = await handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
}
```

## Notes

- All handlers use `CancellationToken` for async operations
- DTOs use `IReadOnlyCollection<T>` for collections
- Repository implementations are in Infrastructure layer
- Mapster configuration is centralized in `MapsterConfiguration.cs`
