using Social_Taxi.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Social_Taxi.EntityFramework.Configurations
{
    public class AdditionalRightConfiguration : IEntityTypeConfiguration<AdditionalRight>
    {
        public void Configure(EntityTypeBuilder<AdditionalRight> builder)
        {
            builder.ToTable("AdditionalRight").HasKey(a => a.AdditionalRidhtId);
            builder.HasOne(a => a.Right).WithMany(r => r.AdditionalRights).HasForeignKey(a => a.RightId);
            builder.HasOne(a => a.User).WithMany(r => r.AdditionalRights).HasForeignKey(a => a.UserId);
        }
    }
}
