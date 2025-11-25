namespace VistaPms.Application.Features.RoomTypes.DTOs;

public class RoomTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int DefaultCapacity { get; set; }
    public List<RoomAmenityDto> Amenities { get; set; } = new();
    public List<RoomTypeImageDto> Images { get; set; } = new();
}
