using MediatR;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.RoomFeatures.Commands.DeleteRoomFeature;

public record DeleteRoomFeatureCommand(Guid Id) : IRequest;

public class DeleteRoomFeatureCommandHandler : IRequestHandler<DeleteRoomFeatureCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoomFeatureCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRoomFeatureCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomFeatures
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RoomFeature), request.Id);
        }

        _context.RoomFeatures.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
