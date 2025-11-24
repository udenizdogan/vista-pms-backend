using VistaPms.Domain.Enums;

namespace VistaPms.Application.DTOs.Reservations;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public Guid GuestId { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public int TotalNights { get; set; }
    public ReservationStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public string? RatePlanName { get; set; }
}
