using CSharp_React.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_React.EntityFramework.Configurations
{
    public class ResponseConfiguration: IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.ToTable("Response").HasKey(a => a.ResponseDriverId);
           // builder.HasOne(a => a.Ride).WithMany(r => r.ResponseDrivers).HasForeignKey(a => a.RideId);
            builder.HasOne(a => a.User).WithMany(r => r.Response).HasForeignKey(a => a.ResponseUserId).IsRequired(false);


        }
    }
}
