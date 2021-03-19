using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkateboardNeverDie.Domain.Tricks;

namespace SkateboardNeverDie.Infrastructure.Domain.Tricks
{
    public sealed class TrickEntityTypeConfiguration : IEntityTypeConfiguration<Trick>
    {
        public void Configure(EntityTypeBuilder<Trick> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Description)
                .IsUnicode()
                .HasMaxLength(512);

            builder.HasIndex("Id", "Name").IsUnique();
        }
    }
}
