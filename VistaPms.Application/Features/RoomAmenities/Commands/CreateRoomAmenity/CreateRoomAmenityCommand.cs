using FluentValidation;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.RoomAmenities;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomAmenities.Commands.CreateRoomAmenity;

public record CreateRoomAmenityCommand(CreateRoomAmenityRequest Request) : IRequest<Guid>;

public class CreateRoomAmenityCommandValidator : AbstractValidator<CreateRoomAmenityCommand>
{
    public CreateRoomAmenityCommandValidator()
    {
        RuleFor(v => v.Request.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(v => v.Request.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            
        RuleFor(v => v.Request.Icon)
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
            Name = request.Request.Name,
            Description = request.Request.Description,
            Icon = request.Request.Icon,
            IsActive = request.Request.IsActive
        };

        _context.RoomAmenities.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
