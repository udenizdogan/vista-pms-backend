using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Rooms;

namespace VistaPms.Application.Features.Rooms.Commands.UpdateRoom;

public record UpdateRoomCommand(Guid Id, UpdateRoomRequest Request) : IRequest<bool>;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .Include(r => r.RoomAmenities)
            .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);

        if (room == null) return false;

        room.RoomNumber = command.Request.RoomNumber;
        room.FloorNumber = command.Request.FloorNumber;
        room.BuildingId = command.Request.BuildingId;
        room.RoomTypeId = command.Request.RoomTypeId;
        room.Capacity = command.Request.Capacity;
        room.RoomStatusId = command.Request.RoomStatusId;
        room.Notes = command.Request.Notes;

        // Mevcut RoomAmenity ilişkilerini temizle
        room.RoomAmenities.Clear();

        // Yeni gelen RoomAmenity'leri ekle
        if (command.Request.RoomAmenityIds != null && command.Request.RoomAmenityIds.Any())
        {
            var amenities = await _context.RoomAmenities
                .Where(ra => command.Request.RoomAmenityIds.Contains(ra.Id))
                .ToListAsync(cancellationToken);

            foreach (var amenity in amenities)
            {
                room.RoomAmenities.Add(amenity);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
