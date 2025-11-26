using FluentValidation;
using MediatR;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.RoomFeatures;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomFeatures.Commands.UpdateRoomFeature;

public record UpdateRoomFeatureCommand(Guid Id, UpdateRoomFeatureRequest Request) : IRequest;

public class UpdateRoomFeatureCommandValidator : AbstractValidator<UpdateRoomFeatureCommand>
{
    public UpdateRoomFeatureCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.Request.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(v => v.Request.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(v => v.Request.Icon)
            .MaximumLength(100).WithMessage("Icon must not exceed 100 characters.");
    }
}

public class UpdateRoomFeatureCommandHandler : IRequestHandler<UpdateRoomFeatureCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateRoomFeatureCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateRoomFeatureCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomFeatures
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RoomFeature), request.Id);
        }

        entity.Name = request.Request.Name;
        entity.Description = request.Request.Description;
        entity.Icon = request.Request.Icon;
        entity.IsActive = request.Request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
