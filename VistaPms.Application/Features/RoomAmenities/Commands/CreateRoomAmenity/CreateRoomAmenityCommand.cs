using FluentValidation;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomAmenities.Commands.CreateRoomAmenity;

public record CreateRoomAmenityCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? Icon { get; init; }
    public bool IsActive { get; init; } = true;
}

public class CreateRoomAmenityCommandValidator : AbstractValidator<CreateRoomAmenityCommand>
{
    public CreateRoomAmenityCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(v => v.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            
        RuleFor(v => v.Icon)
            .MaximumLength(100).WithMessage("Icon must not exceed 100 characters.");
    }
}

public class CreateRoomAmenityCommandHandler : IRequestHandler<CreateRoomAmenityCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateRoomAmenityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRoomAmenityCommand request, CancellationToken cancellationToken)
    {
        var entity = new RoomAmenity
        {
            Name = request.Name,
            Description = request.Description,
            Icon = request.Icon,
            IsActive = request.IsActive
        };

        _context.RoomAmenities.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
