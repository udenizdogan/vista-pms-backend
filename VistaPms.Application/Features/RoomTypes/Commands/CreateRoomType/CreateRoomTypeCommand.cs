using FluentValidation;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;
using VistaPms.Domain.ValueObjects;
using VistaPms.Application.Features.RoomTypes.DTOs;

namespace VistaPms.Application.Features.RoomTypes.Commands.CreateRoomType;

public record CreateRoomTypeCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal BasePrice { get; init; }
    public int DefaultCapacity { get; init; }
    public List<RoomFeatureDto> Amenities { get; init; } = new();
    public List<RoomTypeImageDto> Images { get; init; } = new();
}

public class CreateRoomTypeCommandValidator : AbstractValidator<CreateRoomTypeCommand>
{
    public CreateRoomTypeCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(v => v.BasePrice)
            .GreaterThan(0).WithMessage("Base Price must be greater than 0.");

        RuleFor(v => v.DefaultCapacity)
            .GreaterThan(0).WithMessage("Default Capacity must be greater than 0.");
    }
}

public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateRoomTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new RoomType
        {
            Name = request.Name,
            Description = request.Description,
            BasePrice = request.BasePrice,
            DefaultCapacity = request.DefaultCapacity
        };

        foreach (var amenityDto in request.Amenities)
        {
            entity.AddAmenity(new VistaPms.Domain.ValueObjects.RoomFeature(amenityDto.Name, amenityDto.Icon));
        }

        foreach (var imageDto in request.Images)
        {
            entity.AddImage(new RoomTypeImage
            {
                ImageUrl = imageDto.ImageUrl,
                IsMain = imageDto.IsMain,
                Order = imageDto.Order
            });
        }

        _context.RoomTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
