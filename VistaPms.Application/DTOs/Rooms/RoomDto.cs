namespace VistaPms.Application.DTOs.Rooms;

public class RoomDto
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int FloorNumber { get; set; }
    public Guid BuildingId { get; set; }
    public Guid RoomTypeId { get; set; }
    public int Capacity { get; set; }
    public Guid RoomStatusId { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
