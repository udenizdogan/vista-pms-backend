using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Building : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public Guid BuildingStatusId { get; set; }

    // Navigation Property
    public BuildingStatus BuildingStatus { get; set; } = null!;

    private readonly List<Floor> _floors = new();
    public IReadOnlyCollection<Floor> Floors => _floors.AsReadOnly();

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
