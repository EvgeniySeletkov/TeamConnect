namespace TeamConnect.Services.SettingsManager
{
    public interface ISettingsManager
    {
        public int UserId { get; set; }

        public int TeamId { get; set; }

        void ClearSettings();
    }
}
