using Microsoft.EntityFrameworkCore;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Room> Rooms { get; }
    DbSet<RoomAmenity> RoomAmenities { get; }
    DbSet<RoomType> RoomTypes { get; }
    DbSet<RoomTypeImage> RoomTypeImages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
