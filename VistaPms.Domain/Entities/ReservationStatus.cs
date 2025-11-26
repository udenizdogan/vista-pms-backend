using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class ReservationStatus : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; } = true;
}


