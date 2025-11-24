using Ardalis.Result;
using Mapster;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Rooms;

namespace VistaPms.Application.Features.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomByIdQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id);
        
        if (room == null)
        {
            return Result<RoomDto>.NotFound($"Room with ID '{request.Id}' not found");
        }

        var roomDto = room.Adapt<RoomDto>();
        
        return Result<RoomDto>.Success(roomDto);
    }
}
