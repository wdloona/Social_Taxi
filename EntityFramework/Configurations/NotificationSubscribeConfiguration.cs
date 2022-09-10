using CSharp_React.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharp_React.EntityFramework.Configurations
{
    public class NotificationSubscribeConfiguration : IEntityTypeConfiguration<NotificationSubscribe>
    {
        public void Configure(EntityTypeBuilder<NotificationSubscribe> builder)
        {
            builder.ToTable("NotificationSubscribe").HasKey(a => a.NotificationSubscribeId);
            builder.HasOne(a => a.User).WithMany(r => r.NotificationSubscribes).HasForeignKey(a => a.UserId);
        }
    }
}
