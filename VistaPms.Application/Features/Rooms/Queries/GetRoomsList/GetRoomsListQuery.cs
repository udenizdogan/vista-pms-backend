using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Rooms;

namespace VistaPms.Application.Features.Rooms.Queries.GetRoomsList;

public record GetRoomsListQuery : IRequest<Result<IReadOnlyList<RoomDto>>>
{
    public Guid? FloorId { get; init; }
    public Guid? RoomTypeId { get; init; }
    public bool? IsActive { get; init; }
}
