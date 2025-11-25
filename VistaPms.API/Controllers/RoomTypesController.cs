using MediatR;
using Microsoft.AspNetCore.Mvc;
using VistaPms.Application.Features.RoomTypes.Commands.CreateRoomType;
using VistaPms.Application.Features.RoomTypes.Commands.DeleteRoomType;
using VistaPms.Application.Features.RoomTypes.Commands.UpdateRoomType;
using VistaPms.Application.Features.RoomTypes.Queries.GetRoomTypeById;
using VistaPms.Application.Features.RoomTypes.Queries.GetRoomTypes;
using VistaPms.Application.Features.RoomTypes.DTOs;

namespace VistaPms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomTypeDto>>> GetRoomTypes()
    {
        return await _mediator.Send(new GetRoomTypesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomTypeDto>> GetRoomType(Guid id)
    {
        return await _mediator.Send(new GetRoomTypeByIdQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRoomType(CreateRoomTypeCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRoomType(Guid id, UpdateRoomTypeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRoomType(Guid id)
    {
        await _mediator.Send(new DeleteRoomTypeCommand(id));

        return NoContent();
    }
}
