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

            builder.OwnsMany(e => e.UserPermissions, x =>
            {
                x.WithOwner().HasForeignKey(_ => _.PermissionId);

                x.ToTable("UserPermissions");

                x.Property(e => e.PermissionId);
                x.Property(e => e.UserId).IsRequired();
                x.HasKey(e => e.Id);

                x.HasOne(e => e.User);
            });
        }
    }
}
