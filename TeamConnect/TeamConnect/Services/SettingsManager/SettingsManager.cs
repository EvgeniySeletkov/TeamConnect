using Xamarin.Essentials;

namespace TeamConnect.Services.SettingsManager
{
    public class SettingsManager : ISettingsManager
    {
        #region -- ISettingsManager implementation --

        public int UserId
        {
            get => Preferences.Get(nameof(UserId), default(int));
            set => Preferences.Set(nameof(UserId), value);
        }
        public int TeamId
        {
            get => Preferences.Get(nameof(TeamId), default(int));
            set => Preferences.Set(nameof(TeamId), value);
        }

        public void ClearSettings()
        {
            UserId = default(int);
            TeamId = default(int);
        }

        #endregion
    }
}
