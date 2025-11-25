using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rt => rt.Description)
            .HasMaxLength(1000);

        builder.Property(rt => rt.BasePrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(rt => rt.DefaultCapacity)
            .IsRequired();

        builder.Property(rt => rt.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        // Value Objects - Owned Entities
        // Value Objects - Owned Entities
        builder.OwnsMany(rt => rt.Amenities, a =>
        {
            a.ToTable("RoomTypeAmenities");
            a.WithOwner().HasForeignKey("RoomTypeId");
            a.Property(am => am.Name).IsRequired().HasMaxLength(100);
            a.Property(am => am.Icon).HasMaxLength(50);
            a.HasKey("RoomTypeId", "Name");
        });

        builder.HasMany(rt => rt.Rooms)
            .WithOne(r => r.RoomType)
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(rt => rt.Images)
            .WithOne(i => i.RoomType)
            .HasForeignKey(i => i.RoomTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
