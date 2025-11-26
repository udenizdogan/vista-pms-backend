using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Guests;

namespace VistaPms.Application.Features.Guests.Commands.UpdateGuest;

public record UpdateGuestCommand(Guid Id, UpdateGuestRequest Request) : IRequest<Result>;

