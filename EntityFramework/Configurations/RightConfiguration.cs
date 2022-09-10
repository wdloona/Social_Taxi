using CSharp_React.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CSharp_React.EntityFramework.Configurations
{
    public class RightConfiguration : IEntityTypeConfiguration<Right>
    {
        public void Configure(EntityTypeBuilder<Right> builder)
        {
            builder.ToTable("Right").HasKey(r => r.RightId);
        }
    }
}
