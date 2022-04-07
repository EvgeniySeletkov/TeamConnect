using System;
using System.Globalization;
using System.Threading.Tasks;
using TeamConnect.Extensions;
using TeamConnect.Helpers;
using TeamConnect.Models.TimeZone;
using TeamConnect.Services.RestService;

namespace TeamConnect.Services.TimeZoneService
{
    public class TimeZoneService : ITimeZoneService
    {
        private readonly IRestService _restService;

        public TimeZoneService(IRestService restService)
        {
            _restService = restService;
        }

        #region -- ITimeZoneService implementation --

        public async Task<OperationResult<TimeZoneModel>> GetTimeZoneAsync(double latitude, double longitude)
        {
            var result = new OperationResult<TimeZoneModel>();

            try
            {
                var offset = new DateTimeOffset(DateTime.Now);

                var responce = await _restService.GetAsync<TimeZoneModelDTO, object>(
                    $"{Constants.GoogleAPI.BASE_TIMEZONE_API_URI}" +
                    $"{Constants.GoogleAPI.LOCATION}{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}" +
                    $"{Constants.GoogleAPI.TIMESTAMP}{offset.ToUnixTimeSeconds()}" +
                    $"{Constants.GoogleAPI.KEY}{Constants.GoogleAPI.API_KEY}");

                if (responce.IsSuccess)
                {
                    result.SetSuccess(responce.SuccessResult.ToModel());
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetTimeZoneAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
