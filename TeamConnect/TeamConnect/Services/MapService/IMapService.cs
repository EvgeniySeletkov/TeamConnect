using System.Threading.Tasks;
using TeamConnect.Helpers;
using Xamarin.Essentials;

namespace TeamConnect.Services.MapService
{
    public interface IMapService
    {
        Task<OperationResult<Placemark>> GetPlacemarkAsync(double latitude, double longitude);
    }
}
