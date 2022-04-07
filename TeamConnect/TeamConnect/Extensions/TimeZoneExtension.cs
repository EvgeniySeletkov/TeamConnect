using TeamConnect.Models.TimeZone;

namespace TeamConnect.Extensions
{
    public static class TimeZoneExtension
    {
        public static TimeZoneModel ToModel(this TimeZoneModelDTO modelDTO)
        {
            return new TimeZoneModel
            {
                DstOffset = modelDTO.DstOffset,
                RawOffset = modelDTO.RawOffset,
                Status = modelDTO.Status,
                TimeZoneID = modelDTO.TimeZoneID,
                TimeZoneName = modelDTO.TimeZoneName,
            };
        }
    }
}
