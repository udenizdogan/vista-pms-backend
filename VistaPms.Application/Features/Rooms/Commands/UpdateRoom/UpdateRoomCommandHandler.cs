using Ardalis.Result;
using MediatR;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;

namespace VistaPms.Application.Features.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id);
        if (room == null)
        {
            return Result.NotFound($"Room with ID '{request.Id}' not found");
        }

        room.Number = request.Number;
        room.FloorId = request.FloorId;
        room.RoomTypeId = request.RoomTypeId;
        room.Capacity = request.Capacity;
        room.Notes = request.Notes;

        await _roomRepository.UpdateAsync(room);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
