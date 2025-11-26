using Ardalis.Result;
using MediatR;

namespace VistaPms.Application.Features.Reservations.Commands.DeleteReservation;

public record DeleteReservationCommand(Guid Id) : IRequest<Result>;

