using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Guests;

namespace VistaPms.Application.Features.Guests.Commands.CreateGuest;

public record CreateGuestCommand(CreateGuestRequest Request) : IRequest<Result<Guid>>;
