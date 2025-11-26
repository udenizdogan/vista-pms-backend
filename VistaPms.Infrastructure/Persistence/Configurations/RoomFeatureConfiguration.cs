using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class RoomFeatureConfiguration : IEntityTypeConfiguration<RoomFeature>
{
    public void Configure(EntityTypeBuilder<RoomFeature> builder)
    {
        builder.ToTable("RoomFeatures");

        builder.Property(ra => ra.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ra => ra.Description)
            .HasMaxLength(500);

        builder.Property(ra => ra.Icon)
            .HasMaxLength(100);

        // Many-to-Many relationship with Room
        builder.HasMany(ra => ra.Rooms)
            .WithMany(r => r.RoomFeatures)
            .UsingEntity(j => j.ToTable("RoomFeatureRooms"));
    }
}
