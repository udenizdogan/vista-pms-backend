using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Repositories;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Reservation>> GetByGuestIdAsync(
        Guid guestId, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Room)
                .ThenInclude(room => room.RoomType)
            .Where(r => r.GuestId == guestId)
            .OrderByDescending(r => r.CheckIn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Reservation>> GetByDateRangeAsync(
        DateTime startDate, 
        DateTime endDate, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Room)
                .ThenInclude(room => room.RoomType)
            .Where(r => r.CheckIn >= startDate && r.CheckIn <= endDate)
            .OrderBy(r => r.CheckIn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Reservation>> GetActiveReservationsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Room)
                .ThenInclude(room => room.RoomType)
            .Include(r => r.ReservationStatus)
            .Where(r => r.ReservationStatus.Name == "Confirmed" || 
                       r.ReservationStatus.Name == "CheckedIn")
            .OrderBy(r => r.CheckIn)
            .ToListAsync(cancellationToken);
    }
}
