using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class HousekeepingTaskConfiguration : IEntityTypeConfiguration<HousekeepingTask>
{
    public void Configure(EntityTypeBuilder<HousekeepingTask> builder)
    {
        builder.HasKey(ht => ht.Id);

        builder.Property(ht => ht.HousekeepingTaskTypeId)
            .IsRequired();

        builder.Property(ht => ht.MaintenanceStatusId)
            .IsRequired();

        builder.Property(ht => ht.AssignedUserId)
            .HasMaxLength(200);

        builder.Property(ht => ht.DueDate)
            .IsRequired();

        builder.Property(ht => ht.Notes)
            .HasMaxLength(1000);

        builder.Property(ht => ht.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(ht => ht.HousekeepingTaskType)
            .WithMany()
            .HasForeignKey(ht => ht.HousekeepingTaskTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ht => ht.MaintenanceStatus)
            .WithMany()
            .HasForeignKey(ht => ht.MaintenanceStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ht => ht.Room)
            .WithMany()
            .HasForeignKey(ht => ht.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
