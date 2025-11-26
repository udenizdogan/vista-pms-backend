using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result<Guid>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationDbContext _context;

    public CreateReservationCommandHandler(
        IReservationRepository reservationRepository,
        IRoomRepository roomRepository,
        IUnitOfWork unitOfWork,
        IApplicationDbContext context)
    {
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        _context = context;
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

        var pendingStatus = await _context.Set<ReservationStatus>()
            .FirstOrDefaultAsync(s => s.Name == "Pending", cancellationToken);

        if (pendingStatus == null)
        {
            return Result<Guid>.Error("Reservation status 'Pending' not found");
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
            ReservationStatusId = pendingStatus.Id,
            TotalPrice = 0 // Calculate based on rate plan
        };

        await _reservationRepository.AddAsync(reservation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(reservation.Id);
    }
}
