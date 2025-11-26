using FluentValidation;

namespace VistaPms.Application.Features.Reservations.Commands.UpdateReservation;

public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
{
    public UpdateReservationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Reservation ID is required");

        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("Room is required");

        RuleFor(x => x.GuestId)
            .NotEmpty().WithMessage("Guest is required");

        RuleFor(x => x.CheckIn)
            .NotEmpty().WithMessage("Check-in date is required");

        RuleFor(x => x.CheckOut)
            .NotEmpty().WithMessage("Check-out date is required")
            .GreaterThan(x => x.CheckIn).WithMessage("Check-out date must be after check-in date");

        RuleFor(x => x.Adults)
            .GreaterThan(0).WithMessage("At least 1 adult is required");

        RuleFor(x => x.Children)
            .GreaterThanOrEqualTo(0).WithMessage("Children count cannot be negative");

        RuleFor(x => x.RatePlanId)
            .NotEmpty().WithMessage("Rate plan is required");

        RuleFor(x => x.ReservationStatusId)
            .NotEmpty().WithMessage("Reservation status is required");
    }
}

