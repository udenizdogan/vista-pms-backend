using Ardalis.Result;
using MediatR;

namespace VistaPms.Application.Features.Rooms.Commands.UpdateRoom;

public record UpdateRoomCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Number { get; init; } = string.Empty;
    public Guid FloorId { get; init; }
    public Guid RoomTypeId { get; init; }
    public int Capacity { get; init; }
    public string? Notes { get; init; }
}
