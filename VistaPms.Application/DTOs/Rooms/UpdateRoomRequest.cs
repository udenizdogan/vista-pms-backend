using VistaPms.Domain.Enums;

namespace VistaPms.Application.DTOs.Rooms;

public class UpdateRoomRequest
{
    public string RoomNumber { get; set; } = string.Empty;
    public int FloorNumber { get; set; }
    public Guid RoomTypeId { get; set; }
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; }
    public string? Notes { get; set; }
}
