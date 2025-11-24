using FluentValidation;

namespace VistaPms.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Room number is required")
            .MaximumLength(20).WithMessage("Room number must not exceed 20 characters");

        RuleFor(x => x.FloorId)
            .NotEmpty().WithMessage("Floor is required");

        RuleFor(x => x.RoomTypeId)
            .NotEmpty().WithMessage("Room type is required");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0")
            .LessThanOrEqualTo(10).WithMessage("Capacity must not exceed 10");
    }
}
