using VistaPms.Domain.Enums;

namespace VistaPms.Domain.Entities;

public class FolioPayment : BaseEntity
{
    public Guid FolioId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string? ReferenceNumber { get; set; }

    public Folio Folio { get; set; } = null!;
}
