using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Description)
            .HasMaxLength(1000);

        builder.Property(b => b.Address)
            .HasMaxLength(500);

        builder.Property(b => b.BuildingStatusId)
            .IsRequired();

        builder.Property(b => b.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        // Relationships
        builder.HasOne(b => b.BuildingStatus)
            .WithMany(bs => bs.Buildings)
            .HasForeignKey(b => b.BuildingStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.Floors)
            .WithOne(f => f.Building)
            .HasForeignKey(f => f.BuildingId);

        builder.HasMany(b => b.Rooms)
            .WithOne(r => r.Building)
            .HasForeignKey(r => r.BuildingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
