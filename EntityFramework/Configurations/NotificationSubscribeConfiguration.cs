using Social_Taxi.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Social_Taxi.EntityFramework.Configurations
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
