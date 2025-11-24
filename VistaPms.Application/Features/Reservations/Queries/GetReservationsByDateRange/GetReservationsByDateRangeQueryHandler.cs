using Ardalis.Result;
using Mapster;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Reservations;

namespace VistaPms.Application.Features.Reservations.Queries.GetReservationsByDateRange;

public class GetReservationsByDateRangeQueryHandler : IRequestHandler<GetReservationsByDateRangeQuery, Result<IReadOnlyList<ReservationDto>>>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationsByDateRangeQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<IReadOnlyList<ReservationDto>>> Handle(GetReservationsByDateRangeQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetByDateRangeAsync(
            request.StartDate, 
            request.EndDate, 
            cancellationToken);

        var reservationDtos = reservations.Adapt<List<ReservationDto>>();

        return Result<IReadOnlyList<ReservationDto>>.Success(reservationDtos);
    }
}
