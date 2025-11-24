using VistaPms.Domain.Enums;
using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Room : BaseEntity, IAggregateRoot
{
    public string Number { get; set; } = string.Empty;
    public Guid FloorId { get; set; }
    public Guid RoomTypeId { get; set; }
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; } = RoomStatus.Vacant;
    public HousekeepingStatus HousekeepingStatus { get; set; } = HousekeepingStatus.Clean;
    public bool IsActive { get; set; } = true;
    public Guid? ActiveRatePlanId { get; set; }
    public string? Notes { get; set; }

    public Floor Floor { get; set; } = null!;
    public RoomType RoomType { get; set; } = null!;
    public RatePlan? ActiveRatePlan { get; set; }

    private readonly List<Reservation> _reservations = new();
    public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();

    private readonly List<Product> _minibarItems = new();
    public IReadOnlyCollection<Product> MinibarItems => _minibarItems.AsReadOnly();

    private readonly List<MaintenanceTicket> _maintenanceTickets = new();
    public IReadOnlyCollection<MaintenanceTicket> MaintenanceTickets => _maintenanceTickets.AsReadOnly();

    private readonly List<HousekeepingTask> _housekeepingTasks = new();
    public IReadOnlyCollection<HousekeepingTask> HousekeepingTasks => _housekeepingTasks.AsReadOnly();
}
