using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.Features.RoomTypes.DTOs;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomTypes.Queries.GetRoomTypeById;

public record GetRoomTypeByIdQuery(Guid Id) : IRequest<RoomTypeDto>;

public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, RoomTypeDto>
{
    private readonly IApplicationDbContext _context;

    public GetRoomTypeByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoomTypeDto> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomTypes
            .Include(rt => rt.Images)
            .FirstOrDefaultAsync(rt => rt.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RoomType), request.Id);
        }

        return new RoomTypeDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            BasePrice = entity.BasePrice,
            DefaultCapacity = entity.DefaultCapacity,
            Amenities = entity.Amenities.Select(a => new RoomAmenityDto
            {
                Name = a.Name,
                Icon = a.Icon
            }).ToList(),
            Images = entity.Images.Select(i => new RoomTypeImageDto
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                IsMain = i.IsMain,
                Order = i.Order
            }).OrderBy(i => i.Order).ToList()
        };
    }
}
