using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(g => g.Phone)
            .HasMaxLength(20);

        builder.Property(g => g.Address)
            .HasMaxLength(500);

        builder.Property(g => g.Nationality)
            .HasMaxLength(100);

        builder.Property(g => g.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        // Computed property
        builder.Ignore(g => g.FullName);

        // Unique index for email per tenant
        builder.HasIndex(g => new { g.TenantId, g.Email })
            .IsUnique()
            .HasDatabaseName("IX_Guests_TenantId_Email");

        // Index for name searches
        builder.HasIndex(g => new { g.LastName, g.FirstName })
            .HasDatabaseName("IX_Guests_Name");

        // Relationships
        builder.HasMany(g => g.Reservations)
            .WithOne(r => r.Guest)
            .HasForeignKey(r => r.GuestId);

        builder.HasMany(g => g.Folios)
            .WithOne()
            .HasForeignKey("GuestId");
    }
}
