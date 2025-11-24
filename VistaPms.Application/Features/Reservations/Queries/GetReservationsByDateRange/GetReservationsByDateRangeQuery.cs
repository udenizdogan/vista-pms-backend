using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Reservations;

namespace VistaPms.Application.Features.Reservations.Queries.GetReservationsByDateRange;

public record GetReservationsByDateRangeQuery : IRequest<Result<IReadOnlyList<ReservationDto>>>
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}
