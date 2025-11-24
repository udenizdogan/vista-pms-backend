namespace VistaPms.Domain.ValueObjects;

public class RoomAmenity
{
    public string Name { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;

    public RoomAmenity(string name, string icon)
    {
        Name = name;
        Icon = icon;
    }

    protected RoomAmenity() { }
}
