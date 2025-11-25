using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class RatePlanConfiguration : IEntityTypeConfiguration<RatePlan>
{
    public void Configure(EntityTypeBuilder<RatePlan> builder)
    {
        builder.HasKey(rp => rp.Id);

        builder.Property(rp => rp.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(rp => rp.Description)
            .HasMaxLength(1000);

        builder.Property(rp => rp.PricePerNight)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(rp => rp.StartDate)
            .IsRequired();

        builder.Property(rp => rp.EndDate)
            .IsRequired();

        builder.Property(rp => rp.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        // Value Objects - Owned Entities
        builder.OwnsMany(rp => rp.CancellationPolicies, cp =>
        {
            cp.Property(c => c.DaysBeforeCheckIn).IsRequired();
            cp.Property(c => c.PenaltyPercentage).IsRequired().HasColumnType("decimal(5,2)");
        });

        builder.HasOne(rp => rp.RoomType)
            .WithMany()
            .HasForeignKey(rp => rp.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(rp => rp.Reservations)
            .WithOne(r => r.RatePlan)
            .HasForeignKey(r => r.RatePlanId);
    }
}
