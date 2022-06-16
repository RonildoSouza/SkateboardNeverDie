using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkateboardNeverDie.Domain.Stances;
using System;

namespace SkateboardNeverDie.Infrastructure.Domain.Skaters
{
    public sealed class StanceEntityTypeConfiguration : IEntityTypeConfiguration<Stance>
    {
        public void Configure(EntityTypeBuilder<Stance> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasConversion(v => v.ToString(), v => (StanceType)Enum.Parse(typeof(StanceType), v));

            builder.Property(e => e.Description)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(512);
        }
    }
}
