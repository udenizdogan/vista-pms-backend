using VistaPms.Domain.Interfaces;
using VistaPms.Domain.ValueObjects;
using ValueObjectRoomAmenity = VistaPms.Domain.ValueObjects.RoomAmenity;

namespace VistaPms.Domain.Entities;

public class RoomType : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int DefaultCapacity { get; set; }

    private readonly List<ValueObjectRoomAmenity> _amenities = new();
    public IReadOnlyCollection<ValueObjectRoomAmenity> Amenities => _amenities.AsReadOnly();

    private readonly List<Room> _rooms = new();
    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

    private readonly List<RatePlan> _ratePlans = new();
    public IReadOnlyCollection<RatePlan> RatePlans => _ratePlans.AsReadOnly();

    private readonly List<RoomTypeImage> _images = new();
    public IReadOnlyCollection<RoomTypeImage> Images => _images.AsReadOnly();

    public void AddAmenity(ValueObjectRoomAmenity amenity)
    {
        if (!_amenities.Any(a => a.Name == amenity.Name))
        {
            _amenities.Add(amenity);
        }
    }

    public void RemoveAmenity(string amenityName)
    {
        var amenity = _amenities.FirstOrDefault(a => a.Name == amenityName);
        if (amenity != null)
        {
            _amenities.Remove(amenity);
        }
    }

    public void AddImage(RoomTypeImage image)
    {
        _images.Add(image);
    }

    public void RemoveImage(Guid imageId)
    {
        var image = _images.FirstOrDefault(i => i.Id == imageId);
        if (image != null)
        {
            _images.Remove(image);
        }
    }
}
