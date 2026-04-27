using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlotEngine.Domain.Entities;

namespace SlotEngine.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(g => g.ReelCount).IsRequired();
        builder.Property(g => g.RowCount).IsRequired();
        builder.Property(g => g.CurrentConfigVersionId);

        builder.Property(g => g.Status).HasConversion<int>().IsRequired();
        builder.Property(g => g.CreatedAt).IsRequired();
        builder.Property(g => g.UpdatedAt).IsRequired();

        builder.HasMany(g => g.ConfigVersions)
            .WithOne()
            .HasForeignKey(v => v.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(Game.ConfigVersions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
