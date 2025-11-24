using Ardalis.Result;
using Mapster;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<CreateRoomCommandResponse>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateRoomCommandResponse>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        // Check if room number already exists
        var existingRoom = await _roomRepository.GetByNumberAsync(request.Number, cancellationToken);
        if (existingRoom != null)
        {
            return Result<CreateRoomCommandResponse>.Error($"Room with number '{request.Number}' already exists");
        }

        var room = new Room
        {
            Number = request.Number,
            FloorId = request.FloorId,
            RoomTypeId = request.RoomTypeId,
            Capacity = request.Capacity,
            Notes = request.Notes
        };

        await _roomRepository.AddAsync(room);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateRoomCommandResponse
        {
            Id = room.Id,
            Number = room.Number,
            Message = "Room created successfully"
        };

        return Result<CreateRoomCommandResponse>.Success(response);
    }
}
