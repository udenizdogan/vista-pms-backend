using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.RoomAmenities;

namespace VistaPms.Application.Features.RoomAmenities.Queries.GetRoomAmenities;

public record GetRoomAmenitiesQuery : IRequest<IEnumerable<RoomAmenityDto>>
{
    public bool? IsActive { get; init; }
}

public class GetRoomAmenitiesQueryHandler : IRequestHandler<GetRoomAmenitiesQuery, IEnumerable<RoomAmenityDto>>
{
    private readonly IApplicationDbContext _context;

    public GetRoomAmenitiesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RoomAmenityDto>> Handle(GetRoomAmenitiesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.RoomAmenities.AsQueryable();

        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive.Value);
        }

        var amenities = await query.ToListAsync(cancellationToken);
        
        return amenities.Adapt<List<RoomAmenityDto>>();
    }
}
