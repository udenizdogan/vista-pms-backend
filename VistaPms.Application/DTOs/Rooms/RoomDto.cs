using VistaPms.Domain.Enums;

namespace VistaPms.Application.DTOs.Rooms;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public Guid FloorId { get; set; }
    public string FloorName { get; set; } = string.Empty;
    public Guid RoomTypeId { get; set; }
    public string RoomTypeName { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; }
    public HousekeepingStatus HousekeepingStatus { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
    public IReadOnlyCollection<string> Amenities { get; set; } = Array.Empty<string>();
}
