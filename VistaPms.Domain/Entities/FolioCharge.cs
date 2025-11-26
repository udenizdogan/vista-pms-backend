namespace VistaPms.Domain.Entities;

public class FolioCharge : BaseEntity
{
    public Guid FolioId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public Guid? ProductId { get; set; }
    public Guid ChargeTypeId { get; set; }

    public ChargeType ChargeType { get; set; } = null!;
    public Folio Folio { get; set; } = null!;
    public Product? Product { get; set; }
}
