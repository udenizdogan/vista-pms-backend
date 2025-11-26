using MediatR;
using Microsoft.AspNetCore.Mvc;
using VistaPms.Application.DTOs.Guests;
using VistaPms.Application.Features.Guests.Commands.CreateGuest;
using VistaPms.Application.Features.Guests.Commands.DeleteGuest;
using VistaPms.Application.Features.Guests.Commands.UpdateGuest;
using VistaPms.Application.Features.Guests.Queries.GetGuestById;
using VistaPms.Application.Features.Guests.Queries.GetGuests;

namespace VistaPms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GuestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GuestDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetGuestsQuery());
        if (!result.IsSuccess)
        {
            return result.Status == Ardalis.Result.ResultStatus.NotFound 
                ? NotFound(result.Errors) 
                : BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GuestDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetGuestByIdQuery(id));
        if (!result.IsSuccess)
        {
            return result.Status == Ardalis.Result.ResultStatus.NotFound 
                ? NotFound(result.Errors) 
                : BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateGuestRequest request)
    {
        var result = await _mediator.Send(new CreateGuestCommand(request));
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGuestRequest request)
    {
        var result = await _mediator.Send(new UpdateGuestCommand(id, request));
        if (!result.IsSuccess)
        {
            return result.Status == Ardalis.Result.ResultStatus.NotFound 
                ? NotFound(result.Errors) 
                : BadRequest(result.Errors);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteGuestCommand(id));
        if (!result.IsSuccess)
        {
            return result.Status == Ardalis.Result.ResultStatus.NotFound 
                ? NotFound(result.Errors) 
                : BadRequest(result.Errors);
        }
        return NoContent();
    }
}

