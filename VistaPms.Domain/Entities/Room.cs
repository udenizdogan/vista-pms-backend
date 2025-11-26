using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Room : BaseEntity, IAggregateRoot
{
    public string RoomNumber { get; set; } = string.Empty;
    public int FloorNumber { get; set; }
    public Guid BuildingId { get; set; }
    public Guid RoomTypeId { get; set; }
    public int Capacity { get; set; }
    public Guid RoomStatusId { get; set; }
    public string? Notes { get; set; }

    // Navigation Property
    public Building Building { get; set; } = null!;
    public RoomType RoomType { get; set; } = null!;
    public RoomStatus RoomStatus { get; set; } = null!;
    public ICollection<RoomAmenity> RoomAmenities { get; set; } = new List<RoomAmenity>();
}
