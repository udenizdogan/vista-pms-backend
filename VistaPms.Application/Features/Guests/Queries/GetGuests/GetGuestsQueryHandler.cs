using Ardalis.Result;
using Mapster;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Guests;

namespace VistaPms.Application.Features.Guests.Queries.GetGuests;

public class GetGuestsQueryHandler : IRequestHandler<GetGuestsQuery, Result<List<GuestDto>>>
{
    private readonly IGuestRepository _guestRepository;

    public GetGuestsQueryHandler(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<Result<List<GuestDto>>> Handle(GetGuestsQuery request, CancellationToken cancellationToken)
    {
        var guests = await _guestRepository.GetAllAsync();
        
        var guestDtos = guests.Adapt<List<GuestDto>>();
        
        return Result<List<GuestDto>>.Success(guestDtos);
    }
}

