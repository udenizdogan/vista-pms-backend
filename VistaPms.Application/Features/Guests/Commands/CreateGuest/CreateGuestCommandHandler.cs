using Ardalis.Result;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.Guests.Commands.CreateGuest;

public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, Result<Guid>>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGuestCommandHandler(IGuestRepository guestRepository, IUnitOfWork unitOfWork)
    {
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
    {
        // Check if guest with email already exists
        var existingGuest = await _guestRepository.GetByEmailAsync(request.Request.Email, cancellationToken);
        if (existingGuest != null)
        {
            return Result<Guid>.Error($"Guest with email '{request.Request.Email}' already exists");
        }

        var guest = new Guest
        {
            FirstName = request.Request.FirstName,
            LastName = request.Request.LastName,
            Email = request.Request.Email,
            Phone = request.Request.Phone,
            Address = request.Request.Address,
            Nationality = request.Request.Nationality
        };

        await _guestRepository.AddAsync(guest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(guest.Id);
    }
}
