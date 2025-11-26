using Ardalis.Result;
using MediatR;

namespace VistaPms.Application.Features.Guests.Commands.DeleteGuest;

public record DeleteGuestCommand(Guid Id) : IRequest<Result>;

