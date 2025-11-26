namespace VistaPms.Domain.Entities;

public class FolioPayment : BaseEntity
{
    public Guid FolioId { get; set; }
    public decimal Amount { get; set; }
    public Guid PaymentMethodId { get; set; }
    public string? ReferenceNumber { get; set; }

    public PaymentMethod PaymentMethod { get; set; } = null!;
    public Folio Folio { get; set; } = null!;
}
