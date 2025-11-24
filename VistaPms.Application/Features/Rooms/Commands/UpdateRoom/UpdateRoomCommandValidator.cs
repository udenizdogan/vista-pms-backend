using FluentValidation;

namespace VistaPms.Application.Features.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Room ID is required");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Room number is required")
            .MaximumLength(20).WithMessage("Room number must not exceed 20 characters");

        RuleFor(x => x.FloorId)
            .NotEmpty().WithMessage("Floor is required");

        RuleFor(x => x.RoomTypeId)
            .NotEmpty().WithMessage("Room type is required");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0");
    }
}
