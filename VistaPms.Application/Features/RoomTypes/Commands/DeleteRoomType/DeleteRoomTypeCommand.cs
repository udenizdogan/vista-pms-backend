using MediatR;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomTypes.Commands.DeleteRoomType;

public record DeleteRoomTypeCommand(Guid Id) : IRequest;

public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoomTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomTypes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RoomType), request.Id);
        }

        _context.RoomTypes.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
