using VistaPms.Domain.Interfaces;
using VistaPms.Domain.ValueObjects;

namespace VistaPms.Domain.Entities;

public class MaintenanceTicket : BaseEntity, IAggregateRoot
{
    public Guid? RoomId { get; set; }
    public string CreatedByUserId { get; set; } = string.Empty;
    public string? AssignedToUserId { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid MaintenanceStatusId { get; set; }
    public Guid MaintenancePriorityId { get; set; }

    public MaintenanceStatus MaintenanceStatus { get; set; } = null!;
    public MaintenancePriority MaintenancePriority { get; set; } = null!;
    public Room? Room { get; set; }

    private readonly List<MaintenancePhoto> _photos = new();
    public IReadOnlyCollection<MaintenancePhoto> Photos => _photos.AsReadOnly();
}
