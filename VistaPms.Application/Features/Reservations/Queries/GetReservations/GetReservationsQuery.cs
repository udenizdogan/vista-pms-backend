using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Reservations;

namespace VistaPms.Application.Features.Reservations.Queries.GetReservations;

public record GetReservationsQuery : IRequest<Result<List<ReservationDto>>>;

