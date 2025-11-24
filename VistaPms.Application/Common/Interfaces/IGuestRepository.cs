using VistaPms.Domain.Entities;

namespace VistaPms.Application.Common.Interfaces;

public interface IGuestRepository : IRepository<Guest>
{
    Task<Guest?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Guest>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
}
