using Microsoft.EntityFrameworkCore;
using VistaPms.Domain.Entities;

namespace VistaPms.Application.Interfaces;

public interface IApplicationDbContext
{
    // DbSet<Room> Rooms { get; }
    // DbSet<Guest> Guests { get; }
    // DbSet<Reservation> Reservations { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
