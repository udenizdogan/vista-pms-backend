using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class FolioConfiguration : IEntityTypeConfiguration<Folio>
{
    public void Configure(EntityTypeBuilder<Folio> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.FolioNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(f => f.FolioStatusId)
            .IsRequired();

        builder.Property(f => f.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        // Computed property
        builder.Ignore(f => f.Balance);

        builder.HasIndex(f => new { f.TenantId, f.FolioNumber })
            .IsUnique();

        builder.HasOne(f => f.FolioStatus)
            .WithMany()
            .HasForeignKey(f => f.FolioStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Reservation)
            .WithOne(r => r.Folio)
            .HasForeignKey<Folio>(f => f.ReservationId)
            .IsRequired(false);

        builder.HasMany(f => f.Charges)
            .WithOne(c => c.Folio)
            .HasForeignKey(c => c.FolioId);

        builder.HasMany(f => f.Payments)
            .WithOne(p => p.Folio)
            .HasForeignKey(p => p.FolioId);
    }
}
