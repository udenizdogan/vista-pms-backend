using MediatR;
using Microsoft.AspNetCore.Mvc;
using VistaPms.Application.DTOs.Rooms;
using VistaPms.Application.Features.Rooms.Commands.CreateRoom;
using VistaPms.Application.Features.Rooms.Commands.DeleteRoom;
using VistaPms.Application.Features.Rooms.Commands.UpdateRoom;
using VistaPms.Application.Features.Rooms.Queries.GetAllRooms;
using VistaPms.Application.Features.Rooms.Queries.GetRoomById;

namespace VistaPms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllRoomsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRoomByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateRoomRequest request)
    {
        var id = await _mediator.Send(new CreateRoomCommand(request));
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoomRequest request)
    {
        var result = await _mediator.Send(new UpdateRoomCommand(id, request));
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteRoomCommand(id));
        if (!result) return NotFound();
        return NoContent();
    }
}
