using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class ChargeTypeConfiguration : IEntityTypeConfiguration<ChargeType>
{
    public void Configure(EntityTypeBuilder<ChargeType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.Order).HasDefaultValue(0);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.TenantId).IsRequired().HasMaxLength(50);

        builder.HasIndex(x => new { x.TenantId, x.Name }).IsUnique();
    }
}


