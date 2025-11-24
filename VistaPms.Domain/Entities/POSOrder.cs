using VistaPms.Domain.Enums;
using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class POSOrder : BaseEntity, IAggregateRoot
{
    public Guid? FolioId { get; set; }
    public decimal TotalAmount { get; set; }
    public POSOrderStatus Status { get; set; } = POSOrderStatus.Pending;

    public Folio? Folio { get; set; }

    private readonly List<POSOrderItem> _items = new();
    public IReadOnlyCollection<POSOrderItem> Items => _items.AsReadOnly();

    public void CalculateTotal()
    {
        TotalAmount = _items.Sum(i => i.Total);
    }
}
