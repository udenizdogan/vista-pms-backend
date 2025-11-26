using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(r => r.FloorNumber)
            .IsRequired();

        builder.Property(r => r.BuildingId)
            .IsRequired();

        builder.Property(r => r.Capacity)
            .IsRequired();

        builder.Property(r => r.RoomStatusId)
            .IsRequired();

        builder.Property(r => r.Notes)
            .IsRequired(false);

        // Relationships
        builder.HasOne(r => r.Building)
            .WithMany(b => b.Rooms)
            .HasForeignKey(r => r.BuildingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.RoomType)
            .WithMany()
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.RoomStatus)
            .WithMany()
            .HasForeignKey(r => r.RoomStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
