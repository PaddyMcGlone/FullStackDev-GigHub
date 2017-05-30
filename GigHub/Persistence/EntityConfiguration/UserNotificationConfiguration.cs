using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfiguration
{
    public class UserNotificationConfiguration: EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasRequired(n => n.User)
                .WithMany(n => n.Notifications)
                .WillCascadeOnDelete(false);
        }
    }
}