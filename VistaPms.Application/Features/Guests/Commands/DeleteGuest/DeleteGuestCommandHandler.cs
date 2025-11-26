using Ardalis.Result;
using MediatR;
using VistaPms.Application.Common.Interfaces;

namespace VistaPms.Application.Features.Guests.Commands.DeleteGuest;

public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand, Result>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGuestCommandHandler(IGuestRepository guestRepository, IUnitOfWork unitOfWork)
    {
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
    {
        var guest = await _guestRepository.GetByIdAsync(request.Id);
        
        if (guest == null)
        {
            return Result.NotFound($"Guest with ID '{request.Id}' not found");
        }

        await _guestRepository.DeleteAsync(guest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

