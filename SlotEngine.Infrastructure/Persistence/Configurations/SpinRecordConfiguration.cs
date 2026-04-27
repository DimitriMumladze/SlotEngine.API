using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlotEngine.Domain.Entities;

namespace SlotEngine.Infrastructure.Persistence.Configurations;

public class SpinRecordConfiguration : IEntityTypeConfiguration<SpinRecord>
{
    public void Configure(EntityTypeBuilder<SpinRecord> builder)
    {
        builder.ToTable("SpinRecords");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.PlayerId).IsRequired();
        builder.Property(s => s.GameId).IsRequired();
        builder.Property(s => s.GameConfigVersionId).IsRequired();
        builder.Property(s => s.ClientSpinId).IsRequired();

        builder.Property(s => s.BetAmount).IsRequired();
        builder.Property(s => s.LineCount).IsRequired();
        builder.Property(s => s.TotalPayout).IsRequired();
        builder.Property(s => s.BalanceBefore).IsRequired();
        builder.Property(s => s.BalanceAfter).IsRequired();

        builder.Property(s => s.StopsJson).IsRequired();
        builder.Property(s => s.GridJson).IsRequired();
        builder.Property(s => s.WinsJson).IsRequired();

        builder.Property(s => s.Status).HasConversion<int>().IsRequired();
        builder.Property(s => s.CreatedAt).IsRequired();
        builder.Property(s => s.UpdatedAt).IsRequired();

        builder.HasIndex(s => new { s.PlayerId, s.ClientSpinId }).IsUnique();
        builder.HasIndex(s => s.PlayerId);
        builder.HasIndex(s => s.GameId);

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(s => s.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Game>()
            .WithMany()
            .HasForeignKey(s => s.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<GameConfigVersion>()
            .WithMany()
            .HasForeignKey(s => s.GameConfigVersionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
