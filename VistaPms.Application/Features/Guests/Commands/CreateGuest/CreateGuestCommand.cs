using Ardalis.Result;
using MediatR;

namespace VistaPms.Application.Features.Guests.Commands.CreateGuest;

public record CreateGuestCommand : IRequest<Result<Guid>>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Phone { get; init; }
    public string? Address { get; init; }
    public string? Nationality { get; init; }
}
