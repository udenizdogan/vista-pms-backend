using FluentValidation;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.RoomFeatures;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomFeatures.Commands.CreateRoomFeature;

public record CreateRoomFeatureCommand(CreateRoomFeatureRequest Request) : IRequest<Guid>;

public class CreateRoomFeatureCommandValidator : AbstractValidator<CreateRoomFeatureCommand>
{
    public CreateRoomFeatureCommandValidator()
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

public class CreateRoomFeatureCommandHandler : IRequestHandler<CreateRoomFeatureCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateRoomFeatureCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRoomFeatureCommand request, CancellationToken cancellationToken)
    {
        var entity = new RoomFeature
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            Icon = request.Request.Icon,
            IsActive = request.Request.IsActive
        };

        _context.RoomFeatures.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
