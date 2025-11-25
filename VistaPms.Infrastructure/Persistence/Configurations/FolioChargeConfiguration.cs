using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class FolioChargeConfiguration : IEntityTypeConfiguration<FolioCharge>
{
    public void Configure(EntityTypeBuilder<FolioCharge> builder)
    {
        builder.HasKey(fc => fc.Id);

        builder.Property(fc => fc.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(fc => fc.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(fc => fc.ChargeType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(fc => fc.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(fc => fc.Folio)
            .WithMany(f => f.Charges)
            .HasForeignKey(fc => fc.FolioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fc => fc.Product)
            .WithMany(p => p.FolioCharges)
            .HasForeignKey(fc => fc.ProductId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}
