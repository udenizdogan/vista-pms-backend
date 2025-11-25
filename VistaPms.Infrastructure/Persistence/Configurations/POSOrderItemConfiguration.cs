using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class POSOrderItemConfiguration : IEntityTypeConfiguration<POSOrderItem>
{
    public void Configure(EntityTypeBuilder<POSOrderItem> builder)
    {
        builder.HasKey(poi => poi.Id);

        builder.Property(poi => poi.Quantity)
            .IsRequired();

        builder.Property(poi => poi.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(poi => poi.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(poi => poi.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(poi => poi.POSOrder)
            .WithMany(po => po.Items)
            .HasForeignKey(poi => poi.POSOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(poi => poi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(poi => poi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
