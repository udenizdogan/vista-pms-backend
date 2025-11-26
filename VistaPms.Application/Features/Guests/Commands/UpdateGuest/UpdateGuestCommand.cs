using Ardalis.Result;
using FluentValidation;
using MediatR;
using VistaPms.Application.Common.Exceptions;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Guests;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Features.Guests.Commands.UpdateGuest;

public record UpdateGuestCommand(Guid Id, UpdateGuestRequest Request) : IRequest<Result>;

public class UpdateGuestCommandValidator : AbstractValidator<UpdateGuestCommand>
{
    public UpdateGuestCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Guest ID is required");

        RuleFor(x => x.Request.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

        RuleFor(x => x.Request.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters");
    }
}

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

        await _guestRepository.UpdateAsync(guest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}



