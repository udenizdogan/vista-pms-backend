namespace VistaPms.Application.Features.RoomTypes.DTOs;

public class RoomTypeImageDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public int Order { get; set; }
}
