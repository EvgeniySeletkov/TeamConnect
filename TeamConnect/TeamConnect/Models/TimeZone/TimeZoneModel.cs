namespace TeamConnect.Models.TimeZone
{
    public class TimeZoneModel
    {
        public int DstOffset { get; set; }

        public int RawOffset { get; set; }

        public string Status { get; set; }

        public string TimeZoneID { get; set; }

        public string TimeZoneName { get; set; }
    }
}
