using VistaPms.Domain.Interfaces;
using VistaPms.Domain.ValueObjects;

namespace VistaPms.Domain.Entities;

public class RoomType : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int DefaultCapacity { get; set; }

    // Amenities (Value Object collection)
    private readonly List<ValueObjects.RoomFeature> _amenities = new();
    public IReadOnlyCollection<ValueObjects.RoomFeature> RoomFeatures => _amenities.AsReadOnly();

    public void AddAmenity(ValueObjects.RoomFeature amenity)
    {
        if (!_amenities.Any(a => a.Name == amenity.Name))
        {
            _amenities.Add(amenity);
        }
    }

    public void RemoveAmenity(string name)
    {
        var amenity = _amenities.FirstOrDefault(a => a.Name == name);
        if (amenity != null)
        {
            _amenities.Remove(amenity);
        }
    }

    // Images (Entity collection)
    private readonly List<RoomTypeImage> _roomTypeImages = new();
    public IReadOnlyCollection<RoomTypeImage> RoomTypeImages => _roomTypeImages.AsReadOnly();

    public void AddImage(RoomTypeImage image)
    {
        image.RoomTypeId = this.Id;
        _roomTypeImages.Add(image);
    }

    public void RemoveImage(Guid imageId)
    {
        var image = _roomTypeImages.FirstOrDefault(i => i.Id == imageId);
        if (image != null)
        {
            _roomTypeImages.Remove(image);
        }
    }
}
