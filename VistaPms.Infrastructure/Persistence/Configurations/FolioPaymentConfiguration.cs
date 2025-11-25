using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VistaPms.Domain.Entities;

namespace VistaPms.Infrastructure.Persistence.Configurations;

public class FolioPaymentConfiguration : IEntityTypeConfiguration<FolioPayment>
{
    public void Configure(EntityTypeBuilder<FolioPayment> builder)
    {
        builder.HasKey(fp => fp.Id);

        builder.Property(fp => fp.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(fp => fp.Method)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(fp => fp.ReferenceNumber)
            .HasMaxLength(200);

        builder.Property(fp => fp.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(fp => fp.Folio)
            .WithMany(f => f.Payments)
            .HasForeignKey(fp => fp.FolioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
