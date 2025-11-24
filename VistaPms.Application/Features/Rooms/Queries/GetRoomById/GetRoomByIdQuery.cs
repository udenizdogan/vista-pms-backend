using Ardalis.Result;
using MediatR;
using VistaPms.Application.DTOs.Rooms;

namespace VistaPms.Application.Features.Rooms.Queries.GetRoomById;

public record GetRoomByIdQuery(Guid Id) : IRequest<Result<RoomDto>>;
