using Ardalis.Result;
using Mapster;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Reservations;

namespace VistaPms.Application.Features.Reservations.Queries.GetReservations;

public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, Result<List<ReservationDto>>>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationsQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<List<ReservationDto>>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetAllAsync();
        
        var reservationDtos = reservations.Adapt<List<ReservationDto>>();
        
        return Result<List<ReservationDto>>.Success(reservationDtos);
    }
}

