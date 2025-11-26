using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.RoomFeatures;

namespace VistaPms.Application.Features.RoomFeatures.Queries.GetRoomFeatures;

public record GetRoomFeaturesQuery : IRequest<IEnumerable<RoomFeatureDto>>
{
    public bool? IsActive { get; init; }
}

public class GetRoomFeaturesQueryHandler : IRequestHandler<GetRoomFeaturesQuery, IEnumerable<RoomFeatureDto>>
{
    private readonly IApplicationDbContext _context;

    public GetRoomFeaturesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RoomFeatureDto>> Handle(GetRoomFeaturesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.RoomFeatures.AsQueryable();

        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive.Value);
        }

        var amenities = await query.ToListAsync(cancellationToken);
        
        return amenities.Adapt<List<RoomFeatureDto>>();
    }
}
