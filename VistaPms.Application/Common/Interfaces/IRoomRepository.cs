using VistaPms.Domain.Entities;

namespace VistaPms.Application.Common.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    Task<Room?> GetByNumberAsync(string number, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Room>> GetByFloorIdAsync(Guid floorId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, CancellationToken cancellationToken = default);
}
