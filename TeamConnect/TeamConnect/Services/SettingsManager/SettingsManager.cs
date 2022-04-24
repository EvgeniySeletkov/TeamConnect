using Xamarin.Essentials;

namespace TeamConnect.Services.SettingsManager
{
    public class SettingsManager : ISettingsManager
    {
        #region -- ISettingsManager implementation --

        public int UserId
        {
            get => Preferences.Get(nameof(UserId), 0);
            set => Preferences.Set(nameof(UserId), value);
        }

        public void ClearSettings()
        {
            UserId = default(int);
        }

        #endregion
    }
}
