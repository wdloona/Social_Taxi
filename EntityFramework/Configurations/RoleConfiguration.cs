using Social_Taxi.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Social_Taxi.EntityFramework.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role").HasKey(u => u.RoleId);
            builder.HasMany(r => r.Rights).WithMany(r => r.Roles).UsingEntity(e => e.ToTable("RightRoleLink"));
        }
    }
}
