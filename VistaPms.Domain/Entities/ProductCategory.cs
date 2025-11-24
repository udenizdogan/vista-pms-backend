using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class ProductCategory : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
}
