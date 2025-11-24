using Ardalis.Result;
using MediatR;

namespace VistaPms.Application.Features.Reservations.Commands.CreateReservation;

public record CreateReservationCommand : IRequest<Result<Guid>>
{
    public Guid RoomId { get; init; }
    public Guid GuestId { get; init; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
    public int Adults { get; init; }
    public int Children { get; init; }
    public Guid RatePlanId { get; init; }
}
