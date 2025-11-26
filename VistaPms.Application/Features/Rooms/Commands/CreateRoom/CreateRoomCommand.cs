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
            BuildingId = command.Request.BuildingId,
            RoomTypeId = command.Request.RoomTypeId,
            Capacity = command.Request.Capacity,
            RoomStatusId = command.Request.RoomStatusId,
            Notes = command.Request.Notes
        };

        // RoomAmenity'leri ekle
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

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync(cancellationToken);

        return room.Id;
    }
}
