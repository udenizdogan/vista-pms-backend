using FluentValidation;

namespace VistaPms.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("Room is required");

        RuleFor(x => x.GuestId)
            .NotEmpty().WithMessage("Guest is required");

        RuleFor(x => x.CheckIn)
            .NotEmpty().WithMessage("Check-in date is required")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Check-in date must be today or in the future");

        RuleFor(x => x.CheckOut)
            .NotEmpty().WithMessage("Check-out date is required")
            .GreaterThan(x => x.CheckIn).WithMessage("Check-out date must be after check-in date");

        RuleFor(x => x.Adults)
            .GreaterThan(0).WithMessage("At least 1 adult is required");

        RuleFor(x => x.Children)
            .GreaterThanOrEqualTo(0).WithMessage("Children count cannot be negative");

        RuleFor(x => x.RatePlanId)
            .NotEmpty().WithMessage("Rate plan is required");
    }
}
