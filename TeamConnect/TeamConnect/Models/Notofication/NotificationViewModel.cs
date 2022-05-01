using Prism.Mvvm;
using TeamConnect.Enums;

namespace TeamConnect.Models.Notofication
{
    public class NotificationViewModel : BindableBase
    {
        #region -- Public properties --

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private ENotificationType _type;
        public ENotificationType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private int _teamId;
        public int TeamId
        {
            get => _teamId;
            set => SetProperty(ref _teamId, value);
        }

        #endregion
    }
}
