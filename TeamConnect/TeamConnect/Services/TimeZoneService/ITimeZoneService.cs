using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.TimeZone;

namespace TeamConnect.Services.TimeZoneService
{
    public interface ITimeZoneService
    {
        Task<OperationResult<TimeZoneModel>> GetTimeZoneAsync(double latitude, double longitude);
    }
}
