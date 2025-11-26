namespace VistaPms.Domain.ValueObjects;

public class RoomFeature
{
    public string Name { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;

    public RoomFeature(string name, string icon)
    {
        Name = name;
        Icon = icon;
    }

    protected RoomFeature() { }
}
