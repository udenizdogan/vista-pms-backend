using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;
using VistaPms.Domain.Enums;

namespace VistaPms.Infrastructure.Persistence.Repositories;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Room?> GetByNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(r => r.RoomType)
            .FirstOrDefaultAsync(r => r.RoomNumber == number, cancellationToken);
    }

    public async Task<List<Room>> GetByFloorNumberAsync(int floorNumber, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(r => r.RoomType)
            .Where(r => r.FloorNumber == floorNumber)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(r => r.RoomType)
            .Where(r => r.Status == RoomStatus.Available)
            .ToListAsync(cancellationToken);
    }
}
