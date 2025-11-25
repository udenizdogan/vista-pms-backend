using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;

namespace VistaPms.Application.Features.Rooms.Commands.DeleteRoom;

public record DeleteRoomCommand(Guid Id) : IRequest<bool>;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);

        if (room == null) return false;

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
