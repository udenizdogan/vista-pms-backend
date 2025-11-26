using MediatR;
using Microsoft.AspNetCore.Mvc;
using VistaPms.Application.DTOs.RoomAmenities;
using VistaPms.Application.Features.RoomAmenities.Commands.CreateRoomAmenity;
using VistaPms.Application.Features.RoomAmenities.Commands.DeleteRoomAmenity;
using VistaPms.Application.Features.RoomAmenities.Commands.UpdateRoomAmenity;
using VistaPms.Application.Features.RoomAmenities.Queries.GetRoomAmenities;
using VistaPms.Application.Features.RoomAmenities.Queries.GetRoomAmenityById;

namespace VistaPms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomAmenitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomAmenitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomAmenityDto>>> GetRoomAmenities([FromQuery] bool? isActive = null)
    {
        var query = new GetRoomAmenitiesQuery { IsActive = isActive };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomAmenityDto>> GetRoomAmenity(Guid id)
    {
        var result = await _mediator.Send(new GetRoomAmenityByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRoomAmenity([FromBody] CreateRoomAmenityRequest request)
    {
        var id = await _mediator.Send(new CreateRoomAmenityCommand(request));
        return CreatedAtAction(nameof(GetRoomAmenity), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRoomAmenity(Guid id, [FromBody] UpdateRoomAmenityRequest request)
    {
        await _mediator.Send(new UpdateRoomAmenityCommand(id, request));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRoomAmenity(Guid id)
    {
        await _mediator.Send(new DeleteRoomAmenityCommand(id));

        return NoContent();
    }
}
