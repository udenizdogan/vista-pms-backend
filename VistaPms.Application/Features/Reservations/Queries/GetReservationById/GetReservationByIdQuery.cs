using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Reservations;

namespace VistaPms.Application.Features.Reservations.Queries.GetReservationById;

public record GetReservationByIdQuery(Guid Id) : IRequest<Result<ReservationDto>>;
