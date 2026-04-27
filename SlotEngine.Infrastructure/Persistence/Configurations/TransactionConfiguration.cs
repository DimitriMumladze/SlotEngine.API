using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlotEngine.Domain.Entities;

namespace SlotEngine.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.PlayerId).IsRequired();
        builder.Property(t => t.Type).HasConversion<int>().IsRequired();
        builder.Property(t => t.Delta).IsRequired();
        builder.Property(t => t.BalanceAfter).IsRequired();
        builder.Property(t => t.SpinRecordId);

        builder.Property(t => t.Reason).HasMaxLength(500);

        builder.Property(t => t.Status).HasConversion<int>().IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.UpdatedAt).IsRequired();

        builder.HasIndex(t => t.PlayerId);
        builder.HasIndex(t => t.SpinRecordId);

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(t => t.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SpinRecord>()
            .WithMany()
            .HasForeignKey(t => t.SpinRecordId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
