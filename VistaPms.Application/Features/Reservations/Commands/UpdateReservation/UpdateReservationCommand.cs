using Ardalis.Result;
using MediatR;

namespace VistaPms.Application.Features.Reservations.Commands.UpdateReservation;

public record UpdateReservationCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public Guid RoomId { get; init; }
    public Guid GuestId { get; init; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
    public int Adults { get; init; }
    public int Children { get; init; }
    public Guid RatePlanId { get; init; }
    public Guid ReservationStatusId { get; init; }
}

