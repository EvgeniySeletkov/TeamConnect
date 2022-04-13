using Acr.UserDialogs;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Extensions;
using TeamConnect.Models.TimeZone;
using TeamConnect.Models.User;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.MapService;
using TeamConnect.Services.TimeZoneService;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Maps;

namespace TeamConnect.ViewModels
{
    public class SelectLocationPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly ITimeZoneService _timeZoneService;
        private readonly IMapService _mapService;

        private UserViewModel _user;

        public SelectLocationPageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            IMapService mapService,
            ITimeZoneService timeZoneService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _mapService = mapService;
            _timeZoneService = timeZoneService;
        }

        #region -- Public properties --

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private Pin _pin;
        public Pin Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value);
        }

        private ICommand _selectLocationCommand;
        public ICommand SelectLocationCommand => _selectLocationCommand ??= new AsyncCommand<MapClickedEventArgs>(OnSelectLocationCommandAsync);

        #endregion

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.TryGetValue(Constants.Navigation.USER, out UserModel user))
            {
                _user = user.ToViewModel();
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Address) && string.IsNullOrWhiteSpace(Address))
            {
                Pin = null;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnSelectLocationCommandAsync(MapClickedEventArgs args)
        {
            var position = args.Position;

            await SetLocationAsync(position);

            await SetTimeZoneAsync(position);

            //if (result.IsSuccess)
            //{
            //    var geos = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            //    var geo = geos.FirstOrDefault();
            //    //var countryCode = geo.CountryCode;
            //    var dateTime = GetDateTime(result.Result);
            //    //var dt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, result.Result.TimeZoneID, TimeZoneInfo.Local.Id);
            //    Console.WriteLine();
            //}
        }

        private async Task SetLocationAsync(Position position)
        {
            var getPlacemarkResult = await _mapService.GetPlacemarkAsync(position.Latitude, position.Longitude);

            if (getPlacemarkResult.IsSuccess)
            {
                var placemark = getPlacemarkResult.Result;

                if (placemark.CountryName is not null)
                {
                    var locality = placemark.Locality is not null
                        ? $"{placemark.Locality}, "
                        : string.Empty;
                    var country = placemark.CountryName;
                    var adminArea = placemark.AdminArea is not null
                        ? $", {placemark.AdminArea}"
                        : string.Empty;

                    Pin = new Pin
                    {
                        Label = string.Empty,
                        Position = position,
                    };

                    Address = locality + country + adminArea;

                    _user.Latitude = position.Latitude;
                    _user.Longitude = position.Longitude;
                    _user.Address = Address;
                    _user.CountryCode = placemark.CountryCode;
                }
                else
                {
                    await _userDialogs.AlertAsync(Strings.WrongLocation);
                }
            }
            else
            {
                await _userDialogs.AlertAsync(Strings.WrongLocation);
            }
        }

        private async Task SetTimeZoneAsync(Position position)
        {
            var getTimeZoneResult = await _timeZoneService.GetTimeZoneAsync(position.Latitude, position.Longitude);

            if (getTimeZoneResult.IsSuccess)
            {
                _user.TimeZoneId = getTimeZoneResult.Result.TimeZoneID;
            }
        }

        private DateTime GetDateTime(TimeZoneModel timeZone)
        {
            var dt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.Date + new TimeSpan(9, 0, 0), timeZone.TimeZoneID);

            return dt;
        }

        #endregion
    }
}
