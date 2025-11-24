using VistaPms.Domain.Enums;
using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class HousekeepingTask : BaseEntity, IAggregateRoot
{
    public Guid RoomId { get; set; }
    public string? AssignedUserId { get; set; }
    public HousekeepingTaskType TaskType { get; set; }
    public DateTime DueDate { get; set; }
    public MaintenanceStatus Status { get; set; } = MaintenanceStatus.Open;
    public string? Notes { get; set; }

    public Room Room { get; set; } = null!;
}
