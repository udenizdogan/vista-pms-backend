using MediatR;
using Microsoft.AspNetCore.Mvc;
using VistaPms.Application.DTOs.Reservations;
using VistaPms.Application.Features.Reservations.Commands.CreateReservation;
using VistaPms.Application.Features.Reservations.Commands.DeleteReservation;
using VistaPms.Application.Features.Reservations.Commands.UpdateReservation;
using VistaPms.Application.Features.Reservations.Queries.GetReservationById;
using VistaPms.Application.Features.Reservations.Queries.GetReservations;

namespace VistaPms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetReservationsQuery());
        if (!result.IsSuccess)
        {
            return result.Status == Ardalis.Result.ResultStatus.NotFound 
                ? NotFound(result.Errors) 
                : BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetReservationByIdQuery(id));
        if (!result.IsSuccess)
        {
            return result.Status == Ardalis.Result.ResultStatus.NotFound 
                ? NotFound(result.Errors) 
                : BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateReservationCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReservationCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _mediator.Send(command);
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
        var result = await _mediator.Send(new DeleteReservationCommand(id));
        if (!result.IsSuccess)
        {
            return result.Status == Ardalis.Result.ResultStatus.NotFound 
                ? NotFound(result.Errors) 
                : BadRequest(result.Errors);
        }
        return NoContent();
    }
}

