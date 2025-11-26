using MediatR;
using Microsoft.AspNetCore.Mvc;
using VistaPms.Application.DTOs.RoomFeatures;
using VistaPms.Application.Features.RoomFeatures.Commands.CreateRoomFeature;
using VistaPms.Application.Features.RoomFeatures.Commands.DeleteRoomFeature;
using VistaPms.Application.Features.RoomFeatures.Commands.UpdateRoomFeature;
using VistaPms.Application.Features.RoomFeatures.Queries.GetRoomFeatures;
using VistaPms.Application.Features.RoomFeatures.Queries.GetRoomFeatureById;

namespace VistaPms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomFeaturesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomFeaturesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomFeatureDto>>> GetRoomFeatures([FromQuery] bool? isActive = null)
    {
        var query = new GetRoomFeaturesQuery { IsActive = isActive };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomFeatureDto>> GetRoomFeature(Guid id)
    {
        var result = await _mediator.Send(new GetRoomFeatureByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRoomFeature([FromBody] CreateRoomFeatureRequest request)
    {
        var id = await _mediator.Send(new CreateRoomFeatureCommand(request));
        return CreatedAtAction(nameof(GetRoomFeature), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRoomFeature(Guid id, [FromBody] UpdateRoomFeatureRequest request)
    {
        await _mediator.Send(new UpdateRoomFeatureCommand(id, request));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRoomFeature(Guid id)
    {
        await _mediator.Send(new DeleteRoomFeatureCommand(id));

        return NoContent();
    }
}
