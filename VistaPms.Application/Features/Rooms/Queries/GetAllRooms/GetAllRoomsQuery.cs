using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Rooms;

namespace VistaPms.Application.Features.Rooms.Queries.GetAllRooms;

public record GetAllRoomsQuery : IRequest<List<RoomDto>>;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<RoomDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllRoomsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                FloorNumber = r.FloorNumber,
                RoomTypeId = r.RoomTypeId,
                Capacity = r.Capacity,
                Status = r.Status,
                Notes = r.Notes,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
