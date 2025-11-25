using MediatR;
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
            Status = command.Request.Status,
            Notes = command.Request.Notes
        };

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync(cancellationToken);

        return room.Id;
    }
}
