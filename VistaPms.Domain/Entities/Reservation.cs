using VistaPms.Domain.Enums;
using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Reservation : BaseEntity, IAggregateRoot
{
    public Guid RoomId { get; set; }
    public Guid GuestId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    public Guid RatePlanId { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid? FolioId { get; set; }

    public Room Room { get; set; } = null!;
    public Guest Guest { get; set; } = null!;
    public RatePlan RatePlan { get; set; } = null!;
    public Folio? Folio { get; set; }

    public int TotalNights => (CheckOut - CheckIn).Days;
}
