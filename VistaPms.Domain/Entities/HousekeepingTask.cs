using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class HousekeepingTask : BaseEntity, IAggregateRoot
{
    public Guid RoomId { get; set; }
    public string? AssignedUserId { get; set; }
    public Guid HousekeepingTaskTypeId { get; set; }
    public DateTime DueDate { get; set; }
    public Guid MaintenanceStatusId { get; set; }
    public string? Notes { get; set; }

    public HousekeepingTaskType HousekeepingTaskType { get; set; } = null!;
    public MaintenanceStatus MaintenanceStatus { get; set; } = null!;
    public Room Room { get; set; } = null!;
}
