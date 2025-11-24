using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.DTOs;
using VistaPms.Application.Interfaces;

namespace VistaPms.Application.Features.Rooms.Queries;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<RoomDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllRoomsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _context.Rooms
            .Include(r => r.RoomType)
            .Include(r => r.Floor)
            .Select(r => new RoomDto
            {
                Id = r.Id,
                Number = r.Number,
                RoomTypeId = r.RoomTypeId,
                RoomTypeName = r.RoomType.Name,
                FloorId = r.FloorId,
                FloorName = r.Floor.Name,
                Capacity = r.Capacity,
                Status = r.Status,
                HousekeepingStatus = r.HousekeepingStatus,
                IsActive = r.IsActive,
                Notes = r.Notes
            })
            .ToListAsync(cancellationToken);

        return rooms;
    }
}
