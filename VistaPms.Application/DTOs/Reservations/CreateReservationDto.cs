namespace VistaPms.Application.DTOs.Reservations;

public class CreateReservationDto
{
    public Guid RoomId { get; set; }
    public Guid GuestId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public Guid RatePlanId { get; set; }
}
