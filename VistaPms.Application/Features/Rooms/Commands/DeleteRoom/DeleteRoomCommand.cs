using Ardalis.Result;
using MediatR;

namespace VistaPms.Application.Features.Rooms.Commands.DeleteRoom;

public record DeleteRoomCommand(Guid Id) : IRequest<Result>;
