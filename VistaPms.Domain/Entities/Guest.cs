using VistaPms.Domain.Interfaces;

namespace VistaPms.Domain.Entities;

public class Guest : BaseEntity, IAggregateRoot
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Nationality { get; set; }

    private readonly List<Reservation> _reservations = new();
    public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();

    private readonly List<Folio> _folios = new();
    public IReadOnlyCollection<Folio> Folios => _folios.AsReadOnly();

    public string FullName => $"{FirstName} {LastName}";
}
