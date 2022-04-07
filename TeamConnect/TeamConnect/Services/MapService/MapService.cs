using System;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using Xamarin.Essentials;

namespace TeamConnect.Services.MapService
{
    public class MapService : IMapService
    {
        #region -- IMapService implementation --

        public async Task<OperationResult<Placemark>> GetPlacemarkAsync(double latitude, double longitude)
        {
            var result = new OperationResult<Placemark>();

            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);

                if (placemarks.ToList().Count > 0)
                {
                    result.SetSuccess(placemarks?.FirstOrDefault());
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetPlacemarkAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
