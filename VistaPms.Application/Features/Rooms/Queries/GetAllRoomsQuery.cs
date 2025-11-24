using MediatR;
using VistaPms.Application.DTOs;

namespace VistaPms.Application.Features.Rooms.Queries;

public record GetAllRoomsQuery : IRequest<List<RoomDto>>;
