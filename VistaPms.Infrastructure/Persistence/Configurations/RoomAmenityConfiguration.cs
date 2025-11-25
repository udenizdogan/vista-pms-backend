using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class RoomAmenityConfiguration : IEntityTypeConfiguration<RoomAmenity>
{
    public void Configure(EntityTypeBuilder<RoomAmenity> builder)
    {
        builder.ToTable("RoomAmenities");

        builder.Property(ra => ra.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ra => ra.Description)
            .HasMaxLength(500);

        builder.Property(ra => ra.Icon)
            .HasMaxLength(100);

        // Many-to-Many relationship with Room
        builder.HasMany(ra => ra.Rooms)
            .WithMany(r => r.RoomAmenities)
            .UsingEntity(j => j.ToTable("RoomAmenityRooms"));
    }
}
