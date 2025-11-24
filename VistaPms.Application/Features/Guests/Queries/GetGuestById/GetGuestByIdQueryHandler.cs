using Ardalis.Result;
using Mapster;
using MediatR;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.DTOs.Guests;

namespace VistaPms.Application.Features.Guests.Queries.GetGuestById;

public class GetGuestByIdQueryHandler : IRequestHandler<GetGuestByIdQuery, Result<GuestDto>>
{
    private readonly IGuestRepository _guestRepository;

    public GetGuestByIdQueryHandler(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<Result<GuestDto>> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
    {
        var guest = await _guestRepository.GetByIdAsync(request.Id);
        
        if (guest == null)
        {
            return Result<GuestDto>.NotFound($"Guest with ID '{request.Id}' not found");
        }

        var guestDto = guest.Adapt<GuestDto>();
        
        return Result<GuestDto>.Success(guestDto);
    }
}
