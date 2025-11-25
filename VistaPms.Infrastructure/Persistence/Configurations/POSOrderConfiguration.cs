using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class POSOrderConfiguration : IEntityTypeConfiguration<POSOrder>
{
    public void Configure(EntityTypeBuilder<POSOrder> builder)
    {
        builder.HasKey(po => po.Id);

        builder.Property(po => po.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(po => po.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(po => po.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(po => po.Folio)
            .WithMany()
            .HasForeignKey(po => po.FolioId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasMany(po => po.Items)
            .WithOne(poi => poi.POSOrder)
            .HasForeignKey(poi => poi.POSOrderId);
    }
}
