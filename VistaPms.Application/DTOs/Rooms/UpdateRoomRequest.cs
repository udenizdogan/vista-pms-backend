namespace VistaPms.Application.DTOs.Rooms;

public class UpdateRoomRequest
{
    public string RoomNumber { get; set; } = string.Empty;
    public int FloorNumber { get; set; }
    public Guid RoomTypeId { get; set; }
    public int Capacity { get; set; }
    public Guid RoomStatusId { get; set; }
    public string? Notes { get; set; }
    public List<Guid> RoomFeatureIds { get; set; } = new();
}
