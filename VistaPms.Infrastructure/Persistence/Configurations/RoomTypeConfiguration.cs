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

        // Value Objects - Owned Entities (Amenities)
        builder.OwnsMany(rt => rt.RoomFeatures, a =>
        {
            a.ToTable("RoomTypeAmenities");
            a.WithOwner().HasForeignKey("RoomTypeId");
            a.Property(am => am.Name).IsRequired().HasMaxLength(100);
            a.Property(am => am.Icon).HasMaxLength(50);
            a.HasKey("RoomTypeId", "Name");
        });

        // Images relationship
        builder.HasMany(rt => rt.RoomTypeImages)
            .WithOne(i => i.RoomType)
            .HasForeignKey(i => i.RoomTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
