using Microsoft.EntityFrameworkCore;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Room> Rooms { get; }
    DbSet<RoomFeature> RoomFeatures { get; }
    DbSet<RoomType> RoomTypes { get; }
    DbSet<RoomTypeImage> RoomTypeImages { get; }
    DbSet<Guest> Guests { get; }
    DbSet<Reservation> Reservations { get; }
    DbSet<ReservationStatus> ReservationStatuses { get; }
    DbSet<RatePlan> RatePlans { get; }
    DbSet<RoomStatus> RoomStatuses { get; }
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
