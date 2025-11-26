namespace VistaPms.Application.DTOs.RoomFeatures;

public class UpdateRoomFeatureRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public bool IsActive { get; set; }
}



