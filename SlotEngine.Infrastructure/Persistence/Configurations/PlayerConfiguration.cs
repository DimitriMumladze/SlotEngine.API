using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlotEngine.Domain.Entities;
using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Players");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasIndex(p => p.Username).IsUnique();

        builder.Property(p => p.Balance)
            .HasConversion(c => c.Amount, v => new Coins(v))
            .HasColumnName("Balance")
            .IsRequired();

        builder.Property(p => p.Status).HasConversion<int>().IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();
    }
}
