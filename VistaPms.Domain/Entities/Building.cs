using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Building : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    private readonly List<Floor> _floors = new();
    public IReadOnlyCollection<Floor> Floors => _floors.AsReadOnly();
}
