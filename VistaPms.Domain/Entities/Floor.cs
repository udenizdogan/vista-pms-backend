using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Floor : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid BuildingId { get; set; }

    public Building Building { get; set; } = null!;

    private readonly List<Room> _rooms = new();
    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
}
