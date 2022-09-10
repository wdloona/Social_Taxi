using Social_Taxi.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Social_Taxi.EntityFramework.Configurations
{
    public class RightConfiguration : IEntityTypeConfiguration<Right>
    {
        public void Configure(EntityTypeBuilder<Right> builder)
        {
            builder.ToTable("Right").HasKey(r => r.RightId);
        }
    }
}
