using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkateboardNeverDie.Domain.Security;

namespace SkateboardNeverDie.Infrastructure.Domain.Security
{
    public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.IdentityUserId)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.Email)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(e => e.Email);
        }
    }
}
