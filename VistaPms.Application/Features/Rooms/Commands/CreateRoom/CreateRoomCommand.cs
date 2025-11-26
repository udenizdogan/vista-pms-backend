using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Rooms;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(CreateRoomRequest Request) : IRequest<Guid>;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var room = new Room
        {
            RoomNumber = command.Request.RoomNumber,
            FloorNumber = command.Request.FloorNumber,
            RoomTypeId = command.Request.RoomTypeId,
            Capacity = command.Request.Capacity,
            RoomStatusId = command.Request.RoomStatusId,
            Notes = command.Request.Notes
        };

        // RoomFeature'leri ekle
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

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync(cancellationToken);

        return room.Id;
    }
}
