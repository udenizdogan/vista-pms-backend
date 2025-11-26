using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Rooms;

namespace VistaPms.Application.Features.Rooms.Queries.GetRoomById;

public record GetRoomByIdQuery(Guid Id) : IRequest<RoomDto?>;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomDto?>
{
    private readonly IApplicationDbContext _context;

    public GetRoomByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoomDto?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (room == null) return null;

        return new RoomDto
        {
            Id = room.Id,
            RoomNumber = room.RoomNumber,
            FloorNumber = room.FloorNumber,
            BuildingId = room.BuildingId,
            RoomTypeId = room.RoomTypeId,
            Capacity = room.Capacity,
            RoomStatusId = room.RoomStatusId,
            Notes = room.Notes,
            CreatedAt = room.CreatedAt,
            UpdatedAt = room.UpdatedAt
        };
    }
}
