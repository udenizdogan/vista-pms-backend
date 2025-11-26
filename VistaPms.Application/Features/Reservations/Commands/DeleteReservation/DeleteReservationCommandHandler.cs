using Ardalis.Result;
using MediatR;
using VistaPms.Application.Common.Interfaces;

namespace VistaPms.Application.Features.Reservations.Commands.DeleteReservation;

public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Result>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReservationCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.Id);
        
        if (reservation == null)
        {
            return Result.NotFound($"Reservation with ID '{request.Id}' not found");
        }

        await _reservationRepository.DeleteAsync(reservation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

