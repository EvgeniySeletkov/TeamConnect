using Newtonsoft.Json;
using System;

namespace TeamConnect.Models.TimeZone
{
    [Serializable()]
    public class TimeZoneModelDTO
    {
        [JsonProperty("dstOffset")]
        public int DstOffset { get; set; }

        [JsonProperty("rawOffset")]
        public int RawOffset { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timeZoneId")]
        public string TimeZoneID { get; set; }

        [JsonProperty("timeZoneName")]
        public string TimeZoneName { get; set; }
    }
}
