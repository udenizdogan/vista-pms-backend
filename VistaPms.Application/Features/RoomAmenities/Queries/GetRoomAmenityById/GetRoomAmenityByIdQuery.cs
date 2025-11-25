using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.RoomAmenities;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomAmenities.Queries.GetRoomAmenityById;

public record GetRoomAmenityByIdQuery(Guid Id) : IRequest<RoomAmenityDto>;

public class GetRoomAmenityByIdQueryHandler : IRequestHandler<GetRoomAmenityByIdQuery, RoomAmenityDto>
{
    private readonly IApplicationDbContext _context;

    public GetRoomAmenityByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoomAmenityDto> Handle(GetRoomAmenityByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomAmenities
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RoomAmenity), request.Id);
        }

        return entity.Adapt<RoomAmenityDto>();
    }
}
