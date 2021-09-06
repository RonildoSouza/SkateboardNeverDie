using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkateboardNeverDie.Domain.Security;

namespace SkateboardNeverDie.Infrastructure.Domain.Security
{
    public sealed class UserPermissionEntityTypeConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
