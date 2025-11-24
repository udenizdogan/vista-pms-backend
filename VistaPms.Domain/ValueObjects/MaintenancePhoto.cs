namespace VistaPms.Domain.ValueObjects;

public class MaintenancePhoto
{
    public string Url { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }

    public MaintenancePhoto(string url, DateTime createdAt)
    {
        Url = url;
        CreatedAt = createdAt;
    }

    protected MaintenancePhoto() { }
}
