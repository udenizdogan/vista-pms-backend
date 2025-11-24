using MediatR;

namespace VistaPms.Application.Features.Rooms.Commands;

public record CreateRoomCommand : IRequest<Guid>
{
    public string Number { get; init; } = string.Empty;
    public Guid FloorId { get; init; }
    public Guid RoomTypeId { get; init; }
    public int Capacity { get; init; }
    public string? Notes { get; init; }
}
