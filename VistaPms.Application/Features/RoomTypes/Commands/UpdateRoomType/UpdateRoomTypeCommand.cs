using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;
using VistaPms.Domain.ValueObjects;
using VistaPms.Application.Features.RoomTypes.DTOs;

namespace VistaPms.Application.Features.RoomTypes.Commands.UpdateRoomType;

public record UpdateRoomTypeCommand : IRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal BasePrice { get; init; }
    public int DefaultCapacity { get; init; }
    public List<RoomAmenityDto> Amenities { get; init; } = new();
    public List<RoomTypeImageDto> Images { get; init; } = new();
}

public class UpdateRoomTypeCommandValidator : AbstractValidator<UpdateRoomTypeCommand>
{
    public UpdateRoomTypeCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
        RuleFor(v => v.BasePrice).GreaterThan(0);
        RuleFor(v => v.DefaultCapacity).GreaterThan(0);
    }
}

public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateRoomTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomTypes
            .Include(rt => rt.Images)
            // Amenities are owned, so they are loaded automatically or we might need to ensure they are tracked
            .FirstOrDefaultAsync(rt => rt.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RoomType), request.Id);
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.BasePrice = request.BasePrice;
        entity.DefaultCapacity = request.DefaultCapacity;

        // Update Amenities
        // Since we don't have a clear ID for amenities (Value Object), we can clear and re-add
        // But RoomType has methods for this.
        // A simpler approach for Value Objects collection is to clear and re-add if the collection is small.
        // However, EF Core OwnsMany handling can be tricky with clear/add.
        // Let's try to sync.
        
        // Clear existing amenities (we need to access the backing field or use methods if available)
        // The entity has RemoveAmenity method but iterating and removing might be slow.
        // Let's assume we can replace the collection if we had access, but we exposed IReadOnlyCollection.
        // We need to use the methods.
        
        var existingAmenities = entity.Amenities.ToList();
        foreach (var amenity in existingAmenities)
        {
            entity.RemoveAmenity(amenity.Name);
        }

        foreach (var amenityDto in request.Amenities)
        {
            entity.AddAmenity(new VistaPms.Domain.ValueObjects.RoomAmenity(amenityDto.Name, amenityDto.Icon));
        }

        // Update Images
        // Images are Entities, so we should try to preserve IDs if possible.
        var existingImages = entity.Images.ToList();
        
        // Identify images to remove
        var imagesToRemove = existingImages.Where(i => !request.Images.Any(ri => ri.Id == i.Id && ri.Id != Guid.Empty)).ToList();
        foreach (var image in imagesToRemove)
        {
            entity.RemoveImage(image.Id);
        }

        // Identify images to add or update
        foreach (var imageDto in request.Images)
        {
            var existingImage = existingImages.FirstOrDefault(i => i.Id == imageDto.Id && imageDto.Id != Guid.Empty);
            if (existingImage != null)
            {
                // Update
                existingImage.ImageUrl = imageDto.ImageUrl;
                existingImage.IsMain = imageDto.IsMain;
                existingImage.Order = imageDto.Order;
            }
            else
            {
                // Add
                entity.AddImage(new RoomTypeImage
                {
                    ImageUrl = imageDto.ImageUrl,
                    IsMain = imageDto.IsMain,
                    Order = imageDto.Order
                });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
