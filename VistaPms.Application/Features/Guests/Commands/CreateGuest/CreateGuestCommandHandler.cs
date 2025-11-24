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
        var existingGuest = await _guestRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingGuest != null)
        {
            return Result<Guid>.Error($"Guest with email '{request.Email}' already exists");
        }

        var guest = new Guest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            Nationality = request.Nationality
        };

        await _guestRepository.AddAsync(guest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(guest.Id);
    }
}
