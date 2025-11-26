using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Guests;

namespace VistaPms.Application.Features.Guests.Queries.GetGuests;

public record GetGuestsQuery : IRequest<Result<List<GuestDto>>>;

