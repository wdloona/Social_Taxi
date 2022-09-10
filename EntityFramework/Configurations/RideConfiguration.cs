using CSharp_React.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CSharp_React.EntityFramework.Configurations
{
    public class RideConfiguration : IEntityTypeConfiguration<Ride>
    {
        public void Configure(EntityTypeBuilder<Ride> builder)
        {
            builder.ToTable("Ride").HasKey(u => u.RideId);
        }
    }
}
