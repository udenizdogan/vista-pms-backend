using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VistaPms.Application.Common.Interfaces;
using VistaPms.Application.Services;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDateTimeProvider dateTimeProvider) : base(options)
    {
        _currentUserService = currentUserService;
        _dateTimeProvider = dateTimeProvider;
    }

    // DbSets - EF Core will discover entities through configurations
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<RoomAmenity> RoomAmenities => Set<RoomAmenity>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<RoomTypeImage> RoomTypeImages => Set<RoomTypeImage>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _dateTimeProvider.UtcNow;
                    entry.Entity.TenantId = _currentUserService.TenantId ?? entry.Entity.TenantId;
                    entry.Entity.BranchId = _currentUserService.BranchId ?? entry.Entity.BranchId;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTimeProvider.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Apply all entity configurations from assembly
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Global Query Filter & Index for Tenant Isolation
        ConfigureTenantFilter(builder);
    }

    private void ConfigureTenantFilter(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(this, new object[] { builder });
            }
        }
    }

    private void SetGlobalQueryFilter<T>(ModelBuilder builder) where T : BaseEntity
    {
        // 1. Tenant ve Branch filtresi
        // - TenantId her zaman eşleşmeli.
        // - BranchId: Eğer kullanıcı tam yetkili ise (Genel Müdür) tüm şubeleri görür,
        //   değilse sadece kendi şubesini görür.
        builder.Entity<T>().HasQueryFilter(e => 
            e.TenantId == _currentUserService.TenantId && 
            (_currentUserService.HasFullAccess || e.BranchId == _currentUserService.BranchId));

        // 2. İndeksler
        builder.Entity<T>().HasIndex(e => e.TenantId);
        builder.Entity<T>().HasIndex(e => e.BranchId);
    }
}
