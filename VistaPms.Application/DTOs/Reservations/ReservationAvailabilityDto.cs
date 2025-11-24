namespace VistaPms.Application.DTOs.Reservations;

public class ReservationAvailabilityDto
{
    public Guid RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string RoomTypeName { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public decimal PricePerNight { get; set; }
    public IReadOnlyCollection<DateTime> UnavailableDates { get; set; } = Array.Empty<DateTime>();
}
