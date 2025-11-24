using MediatR;
using VistaPms.Application.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.Rooms.Commands;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = new Room
        {
            Number = request.Number,
            FloorId = request.FloorId,
            RoomTypeId = request.RoomTypeId,
            Capacity = request.Capacity,
            Notes = request.Notes
        };

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync(cancellationToken);

        return room.Id;
    }
}
