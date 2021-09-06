using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkateboardNeverDie.Domain.Security;

namespace SkateboardNeverDie.Infrastructure.Domain.Security
{
    public sealed class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Description)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(512);

            builder.HasIndex(e => e.Id).IsUnique();

            builder.HasMany(e => e.UserPermissions)
                .WithOne(e => e.Permission)
                .HasForeignKey(e => e.PermissionId);
        }
    }
}
