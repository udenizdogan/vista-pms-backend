using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.Features.RoomTypes.DTOs;

namespace VistaPms.Application.Features.RoomTypes.Queries.GetRoomTypes;

public record GetRoomTypesQuery : IRequest<List<RoomTypeDto>>;

public class GetRoomTypesQueryHandler : IRequestHandler<GetRoomTypesQuery, List<RoomTypeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetRoomTypesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoomTypeDto>> Handle(GetRoomTypesQuery request, CancellationToken cancellationToken)
    {
        return await _context.RoomTypes
            .Include(rt => rt.RoomTypeImages)
            .Select(rt => new RoomTypeDto
            {
                Id = rt.Id,
                Name = rt.Name,
                Description = rt.Description,
                BasePrice = rt.BasePrice,
                DefaultCapacity = rt.DefaultCapacity,
                Amenities = rt.RoomFeatures.Select(a => new RoomFeatureDto
                {
                    Name = a.Name,
                    Icon = a.Icon
                }).ToList(),
                Images = rt.RoomTypeImages.Select(i => new RoomTypeImageDto
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    IsMain = i.IsMain,
                    Order = i.Order
                }).OrderBy(i => i.Order).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
