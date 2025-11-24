using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.TotalPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(r => r.Guest)
            .WithMany(g => g.Reservations)
            .HasForeignKey(r => r.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Room)
            .WithMany(rm => rm.Reservations)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.RatePlan)
            .WithMany(rp => rp.Reservations)
            .HasForeignKey(r => r.RatePlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Folio)
            .WithOne(f => f.Reservation)
            .HasForeignKey<Reservation>(r => r.FolioId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(r => new { r.TenantId, r.CheckIn, r.CheckOut });
    }
}
