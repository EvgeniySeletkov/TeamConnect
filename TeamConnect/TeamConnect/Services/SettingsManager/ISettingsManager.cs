namespace TeamConnect.Services.SettingsManager
{
    public interface ISettingsManager
    {
        public int UserId { get; set; }

        void ClearSettings();
    }
}
