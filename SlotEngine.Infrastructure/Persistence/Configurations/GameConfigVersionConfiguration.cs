using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlotEngine.Domain.Entities;

namespace SlotEngine.Infrastructure.Persistence.Configurations;

public class GameConfigVersionConfiguration : IEntityTypeConfiguration<GameConfigVersion>
{
    public void Configure(EntityTypeBuilder<GameConfigVersion> builder)
    {
        builder.ToTable("GameConfigVersions");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.GameId).IsRequired();
        builder.Property(v => v.Version).IsRequired();

        builder.Property(v => v.ReelStripsJson).IsRequired();
        builder.Property(v => v.PaytableJson).IsRequired();
        builder.Property(v => v.PaylinesJson).IsRequired();

        builder.Property(v => v.CreatedBy)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(v => v.Status).HasConversion<int>().IsRequired();
        builder.Property(v => v.CreatedAt).IsRequired();
        builder.Property(v => v.UpdatedAt).IsRequired();

        builder.HasIndex(v => new { v.GameId, v.Version }).IsUnique();
    }
}
