using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class RoomTypeImage : BaseEntity, IAggregateRoot
{
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public int Order { get; set; }
    public Guid RoomTypeId { get; set; }

    // Navigation Property
    public RoomType RoomType { get; set; } = null!;
}
