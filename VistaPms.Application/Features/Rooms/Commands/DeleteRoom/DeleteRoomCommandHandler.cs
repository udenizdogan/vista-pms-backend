using Ardalis.Result;
using MediatR;
using VistaPms.Application.Common.Interfaces;

namespace VistaPms.Application.Features.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Result>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id);
        if (room == null)
        {
            return Result.NotFound($"Room with ID '{request.Id}' not found");
        }

        await _roomRepository.DeleteAsync(room);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
