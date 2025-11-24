using Ardalis.Result;
using MediatR;

namespace VistaPms.Application.Features.Rooms.Commands.CreateRoom;

public record CreateRoomCommand : IRequest<Result<CreateRoomCommandResponse>>
{
    public string Number { get; init; } = string.Empty;
    public Guid FloorId { get; init; }
    public Guid RoomTypeId { get; init; }
    public int Capacity { get; init; }
    public string? Notes { get; init; }
}
