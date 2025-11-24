namespace VistaPms.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandResponse
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
