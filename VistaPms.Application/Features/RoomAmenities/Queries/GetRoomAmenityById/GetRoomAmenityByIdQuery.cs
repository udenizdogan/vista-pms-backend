using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.RoomFeatures;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomFeatures.Queries.GetRoomFeatureById;

public record GetRoomFeatureByIdQuery(Guid Id) : IRequest<RoomFeatureDto>;

public class GetRoomFeatureByIdQueryHandler : IRequestHandler<GetRoomFeatureByIdQuery, RoomFeatureDto>
{
    private readonly IApplicationDbContext _context;

    public GetRoomFeatureByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoomFeatureDto> Handle(GetRoomFeatureByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomFeatures
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RoomFeature), request.Id);
        }

        return entity.Adapt<RoomFeatureDto>();
    }
}
