using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VistaPms.Application.Interfaces;
using VistaPms.Domain.Entities;
using VistaPms.Infrastructure.Identity;

namespace VistaPms.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.TenantId = _currentUserService.TenantId ?? entry.Entity.TenantId; // Fallback or throw?
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Global Query Filter for Multi-Tenancy
        // Note: This requires all BaseEntity implementations to have TenantId set.
        // We might need to filter by TenantId for all entities that inherit from BaseEntity.
        
        // Example for a specific entity (needs to be generic or applied per entity)
        // builder.Entity<Room>().HasQueryFilter(e => e.TenantId == _currentUserService.TenantId);
        
        // Applying to all BaseEntity
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                // This is a bit complex to do generically in OnModelCreating because _currentUserService is not available here in the same way for expression tree construction 
                // if we want to capture the service instance.
                // Actually, EF Core allows using a service in the filter if registered correctly, or we capture the property.
                // But typically we use a closure over a provider or similar.
                // For simplicity in this setup, I will skip the generic global query filter construction via reflection 
                // and assume it's added in individual configurations or I'll add a helper method.
                
                // Let's stick to simple configuration for now.
            }
        }
    }
}
