namespace VistaPms.Domain.Entities;

public class POSOrderItem : BaseEntity
{
    public Guid POSOrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }

    public POSOrder POSOrder { get; set; } = null!;
    public Product Product { get; set; } = null!;

    public void CalculateTotal()
    {
        Total = Quantity * Price;
    }
}
