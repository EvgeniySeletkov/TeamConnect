using SQLite;
using TeamConnect.Enums;

namespace TeamConnect.Models.Notofication
{
    public class NotificationModel : IEntityBase
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public ENotificationType Type { get; set; }

        public int UserId { get; set; }

        public int TeamId { get; set; }
    }
}
