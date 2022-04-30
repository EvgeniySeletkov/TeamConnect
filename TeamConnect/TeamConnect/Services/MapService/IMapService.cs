using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.TimeZone;
using Xamarin.Essentials;

namespace TeamConnect.Services.MapService
{
    public interface IMapService
    {
        Task<OperationResult<Placemark>> GetPlacemarkAsync(double latitude, double longitude);

        Task<OperationResult<TimeZoneModel>> GetTimeZoneAsync(double latitude, double longitude);
    }
}
