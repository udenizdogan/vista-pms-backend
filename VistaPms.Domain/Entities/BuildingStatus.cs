using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class BuildingStatus : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; } = true;

    private readonly List<Building> _buildings = new();
    public IReadOnlyCollection<Building> Buildings => _buildings.AsReadOnly();
}

