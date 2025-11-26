using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Guests;

namespace VistaPms.Application.Features.Guests.Queries.GetGuestById;

public record GetGuestByIdQuery(Guid Id) : IRequest<Result<GuestDto>>;

