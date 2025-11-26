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
            .Include(r => r.RoomFeatures)
            .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);

        if (room == null) return false;

        room.RoomNumber = command.Request.RoomNumber;
        room.FloorNumber = command.Request.FloorNumber;
        room.RoomTypeId = command.Request.RoomTypeId;
        room.Capacity = command.Request.Capacity;
        room.RoomStatusId = command.Request.RoomStatusId;
        room.Notes = command.Request.Notes;

        // Mevcut RoomFeature ilişkilerini temizle
        room.RoomFeatures.Clear();

        // Yeni gelen RoomFeature'leri ekle
        if (command.Request.RoomFeatureIds != null && command.Request.RoomFeatureIds.Any())
        {
            var amenities = await _context.RoomFeatures
                .Where(ra => command.Request.RoomFeatureIds.Contains(ra.Id))
                .ToListAsync(cancellationToken);

            foreach (var amenity in amenities)
            {
                room.RoomFeatures.Add(amenity);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
