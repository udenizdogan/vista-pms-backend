using VistaPms.Domain.Entities;

namespace VistaPms.Application.Common.Interfaces;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<IReadOnlyList<Reservation>> GetByGuestIdAsync(Guid guestId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reservation>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reservation>> GetActiveReservationsAsync(CancellationToken cancellationToken = default);
}
