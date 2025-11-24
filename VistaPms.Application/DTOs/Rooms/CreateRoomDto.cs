namespace VistaPms.Application.DTOs.Rooms;

public class CreateRoomDto
{
    public string Number { get; set; } = string.Empty;
    public Guid FloorId { get; set; }
    public Guid RoomTypeId { get; set; }
    public int Capacity { get; set; }
    public string? Notes { get; set; }
}
