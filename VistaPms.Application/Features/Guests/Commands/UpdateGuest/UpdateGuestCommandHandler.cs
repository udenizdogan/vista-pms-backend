using Ardalis.Result;
using MediatR;
using VistaPms.Application.Common.Interfaces;

namespace VistaPms.Application.Features.Guests.Commands.UpdateGuest;

public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, Result>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGuestCommandHandler(IGuestRepository guestRepository, IUnitOfWork unitOfWork)
    {
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
    {
        var guest = await _guestRepository.GetByIdAsync(request.Id);
        
        if (guest == null)
        {
            return Result.NotFound($"Guest with ID '{request.Id}' not found");
        }

        // Check if email is being changed and if new email already exists
        if (guest.Email != request.Request.Email)
        {
            var existingGuest = await _guestRepository.GetByEmailAsync(request.Request.Email, cancellationToken);
            if (existingGuest != null && existingGuest.Id != request.Id)
            {
                return Result.Error($"Guest with email '{request.Request.Email}' already exists");
            }
        }

        guest.FirstName = request.Request.FirstName;
        guest.LastName = request.Request.LastName;
        guest.Email = request.Request.Email;
        guest.Phone = request.Request.Phone;
        guest.Address = request.Request.Address;
        guest.Nationality = request.Request.Nationality;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

