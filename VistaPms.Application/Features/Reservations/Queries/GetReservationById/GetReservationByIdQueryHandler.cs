using Ardalis.Result;
using Mapster;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Reservations;

namespace VistaPms.Application.Features.Reservations.Queries.GetReservationById;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, Result<ReservationDto>>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByIdQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<ReservationDto>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.Id);
        
        if (reservation == null)
        {
            return Result<ReservationDto>.NotFound($"Reservation with ID '{request.Id}' not found");
        }

        var reservationDto = reservation.Adapt<ReservationDto>();
        
        return Result<ReservationDto>.Success(reservationDto);
    }
}
