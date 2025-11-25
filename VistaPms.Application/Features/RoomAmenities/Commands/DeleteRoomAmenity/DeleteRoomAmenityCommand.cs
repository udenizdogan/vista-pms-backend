using MediatR;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomAmenities.Commands.DeleteRoomAmenity;

public record DeleteRoomAmenityCommand(Guid Id) : IRequest;

public class DeleteRoomAmenityCommandHandler : IRequestHandler<DeleteRoomAmenityCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoomAmenityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRoomAmenityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomAmenities
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RoomAmenity), request.Id);
        }

        _context.RoomAmenities.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
