using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class MaintenanceTicketConfiguration : IEntityTypeConfiguration<MaintenanceTicket>
{
    public void Configure(EntityTypeBuilder<MaintenanceTicket> builder)
    {
        builder.HasKey(mt => mt.Id);

        builder.Property(mt => mt.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(mt => mt.CreatedByUserId)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(mt => mt.AssignedToUserId)
            .HasMaxLength(200);

        builder.Property(mt => mt.MaintenanceStatusId)
            .IsRequired();

        builder.Property(mt => mt.MaintenancePriorityId)
            .IsRequired();

        builder.Property(mt => mt.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(mt => mt.MaintenanceStatus)
            .WithMany()
            .HasForeignKey(mt => mt.MaintenanceStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mt => mt.MaintenancePriority)
            .WithMany()
            .HasForeignKey(mt => mt.MaintenancePriorityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Value Objects - Owned Entities
        builder.OwnsMany(mt => mt.Photos, p =>
        {
            p.Property(ph => ph.Url).IsRequired().HasMaxLength(500);
            p.Property(ph => ph.CreatedAt).IsRequired();
        });

        builder.HasOne(mt => mt.Room)
            .WithMany()
            .HasForeignKey(mt => mt.RoomId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
