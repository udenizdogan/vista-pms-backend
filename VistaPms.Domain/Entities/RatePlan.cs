using VistaPms.Domain.Interfaces;
using VistaPms.Domain.ValueObjects;

namespace VistaPms.Domain.Entities;

public class RatePlan : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal PricePerNight { get; set; }
    public Guid RoomTypeId { get; set; }
    public int? MinStay { get; set; }
    public int? MaxStay { get; set; }

    public RoomType RoomType { get; set; } = null!;

    private readonly List<CancellationPolicy> _cancellationPolicies = new();
    public IReadOnlyCollection<CancellationPolicy> CancellationPolicies => _cancellationPolicies.AsReadOnly();

    private readonly List<RatePlan> _derivedRates = new();
    public IReadOnlyCollection<RatePlan> DerivedRates => _derivedRates.AsReadOnly();

    private readonly List<Reservation> _reservations = new();
    public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();

    public bool IsActiveOn(DateTime date) => date >= StartDate && date <= EndDate;
}
