using Ardalis.Result;
using Mapster;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Rooms;

namespace VistaPms.Application.Features.Rooms.Queries.GetRoomsList;

public class GetRoomsListQueryHandler : IRequestHandler<GetRoomsListQuery, Result<IReadOnlyList<RoomDto>>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomsListQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<IReadOnlyList<RoomDto>>> Handle(GetRoomsListQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.GetAllAsync();

        // Apply filters
        var filteredRooms = rooms.AsQueryable();

        if (request.FloorId.HasValue)
        {
            filteredRooms = filteredRooms.Where(r => r.FloorId == request.FloorId.Value);
        }

        if (request.RoomTypeId.HasValue)
        {
            filteredRooms = filteredRooms.Where(r => r.RoomTypeId == request.RoomTypeId.Value);
        }

        if (request.IsActive.HasValue)
        {
            filteredRooms = filteredRooms.Where(r => r.IsActive == request.IsActive.Value);
        }

        var roomDtos = filteredRooms.ToList().Adapt<List<RoomDto>>();

        return Result<IReadOnlyList<RoomDto>>.Success(roomDtos);
    }
}
