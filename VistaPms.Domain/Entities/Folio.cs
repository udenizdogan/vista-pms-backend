using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Folio : BaseEntity, IAggregateRoot
{
    public string FolioNumber { get; set; } = string.Empty;
    public Guid? ReservationId { get; set; }
    public Guid GuestId { get; set; }
    public Guid FolioStatusId { get; set; }

    public FolioStatus FolioStatus { get; set; } = null!;
    public Reservation? Reservation { get; set; }
    public Guest Guest { get; set; } = null!;

    private readonly List<FolioCharge> _charges = new();
    public IReadOnlyCollection<FolioCharge> Charges => _charges.AsReadOnly();

    private readonly List<FolioPayment> _payments = new();
    public IReadOnlyCollection<FolioPayment> Payments => _payments.AsReadOnly();

    private readonly List<POSOrder> _posOrders = new();
    public IReadOnlyCollection<POSOrder> POSOrders => _posOrders.AsReadOnly();

    public decimal TotalCharges => _charges.Sum(c => c.Amount);
    public decimal TotalPayments => _payments.Sum(p => p.Amount);
    public decimal Balance => TotalCharges - TotalPayments;
}
