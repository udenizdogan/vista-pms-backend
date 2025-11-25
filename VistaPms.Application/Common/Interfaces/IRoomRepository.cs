using VistaPms.Domain.Entities;

namespace VistaPms.Application.Common.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    Task<Room?> GetByNumberAsync(string number, CancellationToken cancellationToken = default);
    Task<List<Room>> GetByFloorNumberAsync(int floorNumber, CancellationToken cancellationToken = default);
    Task<List<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, CancellationToken cancellationToken = default);
}
