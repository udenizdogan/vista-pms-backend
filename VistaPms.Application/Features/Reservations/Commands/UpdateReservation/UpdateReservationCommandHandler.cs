using Ardalis.Result;
using MediatR;
using VistaPms.Application.Common.Interfaces;

namespace VistaPms.Application.Features.Reservations.Commands.UpdateReservation;

public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Result>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReservationCommandHandler(
        IReservationRepository reservationRepository,
        IRoomRepository roomRepository,
        IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.Id);
        
        if (reservation == null)
        {
            return Result.NotFound($"Reservation with ID '{request.Id}' not found");
        }

        // Check room availability if room or dates are being changed
        if (reservation.RoomId != request.RoomId || 
            reservation.CheckIn != request.CheckIn || 
            reservation.CheckOut != request.CheckOut)
        {
            var availableRooms = await _roomRepository.GetAvailableRoomsAsync(
                request.CheckIn, 
                request.CheckOut, 
                cancellationToken);

            // Allow the same room if it's the current reservation's room
            var isAvailable = availableRooms.Any(r => r.Id == request.RoomId) ||
                              (reservation.RoomId == request.RoomId);

            if (!isAvailable)
            {
                return Result.Error("Room is not available for the selected dates");
            }
        }

        reservation.RoomId = request.RoomId;
        reservation.GuestId = request.GuestId;
        reservation.CheckIn = request.CheckIn;
        reservation.CheckOut = request.CheckOut;
        reservation.Adults = request.Adults;
        reservation.Children = request.Children;
        reservation.RatePlanId = request.RatePlanId;
        reservation.ReservationStatusId = request.ReservationStatusId;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

