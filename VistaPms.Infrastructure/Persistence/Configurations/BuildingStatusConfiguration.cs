using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class BuildingStatusConfiguration : IEntityTypeConfiguration<BuildingStatus>
{
    public void Configure(EntityTypeBuilder<BuildingStatus> builder)
    {
        builder.HasKey(bs => bs.Id);

        builder.Property(bs => bs.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(bs => bs.Description)
            .HasMaxLength(500);

        builder.Property(bs => bs.Order)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(bs => bs.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(bs => bs.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        // Unique index for name per tenant
        builder.HasIndex(bs => new { bs.TenantId, bs.Name })
            .IsUnique()
            .HasDatabaseName("IX_BuildingStatuses_TenantId_Name");

        // Index for ordering
        builder.HasIndex(bs => new { bs.TenantId, bs.Order })
            .HasDatabaseName("IX_BuildingStatuses_TenantId_Order");
    }
}

