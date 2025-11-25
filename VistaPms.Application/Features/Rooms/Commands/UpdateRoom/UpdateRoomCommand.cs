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
            .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);

        if (room == null) return false;

        room.RoomNumber = command.Request.RoomNumber;
        room.FloorNumber = command.Request.FloorNumber;
        room.RoomTypeId = command.Request.RoomTypeId;
        room.Capacity = command.Request.Capacity;
        room.Status = command.Request.Status;
        room.Notes = command.Request.Notes;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
