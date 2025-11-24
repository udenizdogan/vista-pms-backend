using Ardalis.Result;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;
using VistaPms.Domain.Enums;

namespace VistaPms.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result<Guid>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReservationCommandHandler(
        IReservationRepository reservationRepository,
        IRoomRepository roomRepository,
        IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        // Check room availability
        var availableRooms = await _roomRepository.GetAvailableRoomsAsync(
            request.CheckIn, 
            request.CheckOut, 
            cancellationToken);

        if (!availableRooms.Any(r => r.Id == request.RoomId))
        {
            return Result<Guid>.Error("Room is not available for the selected dates");
        }

        var reservation = new Reservation
        {
            RoomId = request.RoomId,
            GuestId = request.GuestId,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            Adults = request.Adults,
            Children = request.Children,
            RatePlanId = request.RatePlanId,
            Status = ReservationStatus.Pending,
            TotalPrice = 0 // Calculate based on rate plan
        };

        await _reservationRepository.AddAsync(reservation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(reservation.Id);
    }
}
