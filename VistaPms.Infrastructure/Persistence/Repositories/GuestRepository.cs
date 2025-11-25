using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Repositories;

public class GuestRepository : GenericRepository<Guest>, IGuestRepository
{
    public GuestRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Guest?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Guests
            .Include(g => g.Reservations)
            .FirstOrDefaultAsync(g => g.Email == email, cancellationToken);
    }

    public async Task<IReadOnlyList<Guest>> SearchByNameAsync(
        string searchTerm, 
        CancellationToken cancellationToken = default)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        
        return await _context.Guests
            .Where(g => g.FirstName.ToLower().Contains(lowerSearchTerm) ||
                       g.LastName.ToLower().Contains(lowerSearchTerm) ||
                       (g.FirstName + " " + g.LastName).ToLower().Contains(lowerSearchTerm))
            .OrderBy(g => g.LastName)
            .ThenBy(g => g.FirstName)
            .ToListAsync(cancellationToken);
    }
}
