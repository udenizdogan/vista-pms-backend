using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.CheckIn)
            .IsRequired()
            .HasColumnName("CheckInDate");

        builder.Property(r => r.CheckOut)
            .IsRequired()
            .HasColumnName("CheckOutDate");

        builder.Property(r => r.Adults)
            .IsRequired();

        builder.Property(r => r.Children)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(r => r.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        // Computed property - not mapped to database
        builder.Ignore(r => r.TotalNights);

        // Indexes
        builder.HasIndex(r => new { r.TenantId, r.CheckIn, r.CheckOut })
            .HasDatabaseName("IX_Reservations_TenantId_Dates");

        builder.HasIndex(r => r.GuestId)
            .HasDatabaseName("IX_Reservations_GuestId");

        builder.HasIndex(r => r.RoomId)
            .HasDatabaseName("IX_Reservations_RoomId");

        builder.HasIndex(r => r.Status)
            .HasDatabaseName("IX_Reservations_Status");

        // Relationships
        builder.HasOne(r => r.Guest)
            .WithMany(g => g.Reservations)
            .HasForeignKey(r => r.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Room)
            .WithMany()
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.RatePlan)
            .WithMany(rp => rp.Reservations)
            .HasForeignKey(r => r.RatePlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Folio)
            .WithOne(f => f.Reservation)
            .HasForeignKey<Reservation>(r => r.FolioId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}
