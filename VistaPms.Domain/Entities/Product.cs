using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Product : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public decimal Price { get; set; }
    public string? Barcode { get; set; }
    public bool IsActive { get; set; } = true;

    public ProductCategory Category { get; set; } = null!;

    private readonly List<POSOrderItem> _orderItems = new();
    public IReadOnlyCollection<POSOrderItem> OrderItems => _orderItems.AsReadOnly();

    private readonly List<FolioCharge> _folioCharges = new();
    public IReadOnlyCollection<FolioCharge> FolioCharges => _folioCharges.AsReadOnly();
}
