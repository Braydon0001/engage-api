using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Domain.Entities.LinkEntities
{
    public class NotificationNotificationChannel
    {
        public int NotificationId { get; set; }
        public int NotificationChannelId { get; set; }

        public Notification Notification { get; set; }
        public NotificationChannel NotificationChannel { get; set; }
    }
}
