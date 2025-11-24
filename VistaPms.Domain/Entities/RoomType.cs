using VistaPms.Domain.Interfaces;
using VistaPms.Domain.ValueObjects;

namespace VistaPms.Domain.Entities;

public class RoomType : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int DefaultCapacity { get; set; }

    private readonly List<RoomAmenity> _amenities = new();
    public IReadOnlyCollection<RoomAmenity> Amenities => _amenities.AsReadOnly();

    private readonly List<Room> _rooms = new();
    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

    private readonly List<RatePlan> _ratePlans = new();
    public IReadOnlyCollection<RatePlan> RatePlans => _ratePlans.AsReadOnly();
}
