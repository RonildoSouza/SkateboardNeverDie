using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkateboardNeverDie.Domain.Skaters;
using System;

namespace SkateboardNeverDie.Infrastructure.Domain.Skaters
{
    public sealed class SkaterEntityTypeConfiguration : IEntityTypeConfiguration<Skater>
    {
        public void Configure(EntityTypeBuilder<Skater> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.LastName)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Nickname)
                .IsUnicode()
                .HasMaxLength(128);

            builder.Property(e => e.Birthdate)
                .IsRequired();

            builder.HasOne(e => e.NaturalStance);

            builder.OwnsMany(e => e.SkaterTricks, x =>
            {
                x.WithOwner().HasForeignKey("SkaterId");

                x.ToTable("SkaterTricks");

                x.Property<Guid>("SkaterId");
                x.Property(e => e.TrickId);
                x.HasKey("Id");

                x.OwnsMany(e => e.SkaterTrickVariations, y =>
                {
                    y.WithOwner().HasForeignKey("SkaterTrickId");

                    y.ToTable("SkaterTrickVariations");

                    y.Property<Guid>("SkaterTrickId");
                    y.Property(e => e.StanceId);
                    y.HasKey("Id");
                });
            });
        }
    }
}
