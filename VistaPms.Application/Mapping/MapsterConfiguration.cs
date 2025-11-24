using Mapster;
using VistaPms.Application.DTOs.Guests;
using VistaPms.Application.DTOs.Reservations;
using VistaPms.Application.DTOs.Rooms;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Mapping;

public static class MapsterConfiguration
{
    public static void Configure()
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(MapsterConfiguration).Assembly);
        
        // Room mappings
        TypeAdapterConfig<Room, RoomDto>
            .NewConfig()
            .Map(dest => dest.FloorName, src => src.Floor.Name)
            .Map(dest => dest.RoomTypeName, src => src.RoomType.Name)
            .Map(dest => dest.BasePrice, src => src.RoomType.BasePrice)
            .Map(dest => dest.Amenities, src => src.RoomType.Amenities.Select(a => a.Name).ToList());

        // Reservation mappings
        TypeAdapterConfig<Reservation, ReservationDto>
            .NewConfig()
            .Map(dest => dest.RoomNumber, src => src.Room.Number)
            .Map(dest => dest.GuestName, src => src.Guest.FullName)
            .Map(dest => dest.TotalNights, src => src.TotalNights)
            .Map(dest => dest.RatePlanName, src => src.RatePlan.Name);

        // Guest mappings
        TypeAdapterConfig<Guest, GuestDto>
            .NewConfig()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.TotalReservations, src => src.Reservations.Count);
    }
}
