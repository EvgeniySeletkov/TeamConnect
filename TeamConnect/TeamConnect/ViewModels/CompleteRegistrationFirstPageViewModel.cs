using Acr.UserDialogs;
using Prism.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.MapService;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Maps;

namespace TeamConnect.ViewModels
{
    public class CompleteRegistrationFirstPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMapService _mapService;

        private UserViewModel _user;

        public CompleteRegistrationFirstPageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            IMapService mapService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _mapService = mapService;
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

        private ICommand _nextTapCommand;
        public ICommand NextTapCommand => _nextTapCommand ??= new AsyncCommand(OnNextTapCommandAsync);

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

            _userDialogs.ShowLoading("Location processing", maskType: MaskType.Clear);

            await SetLocationAsync(position);

            _userDialogs.HideLoading();
        }

        private async Task SetLocationAsync(Position position)
        {
            var getPlacemarkResult = await _mapService.GetPlacemarkAsync(position.Latitude, position.Longitude);

            if (getPlacemarkResult.IsSuccess)
            {
                var placemark = getPlacemarkResult.Result;

                var getTimeZoneResult = await _mapService.GetTimeZoneAsync(position.Latitude, position.Longitude);

                if (placemark.CountryName is not null && getTimeZoneResult.IsSuccess)
                {
                    var locality = placemark.Locality is not null
                        ? $"{placemark.Locality}, "
                        : string.Empty;
                    var country = placemark.CountryName;
                    var adminArea = placemark.AdminArea is not null
                        ? $", {placemark.AdminArea}"
                        : string.Empty;

                    Address = locality + country + adminArea;

                    _user.Latitude = position.Latitude;
                    _user.Longitude = position.Longitude;
                    _user.Address = Address;
                    _user.CountryCode = placemark.CountryCode;
                    _user.TimeZoneId = getTimeZoneResult.Result.TimeZoneID;

                    Pin = new Pin
                    {
                        Label = string.Empty,
                        Position = position,
                    };
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

        private Task OnNextTapCommandAsync()
        {
            var parameters = new NavigationParameters
            {
                { Constants.Navigation.USER, _user.ToModel() },
            };

            return NavigationService.NavigateAsync(nameof(CompleteRegistrationSecondPage), parameters, false, true);
        }

        #endregion
    }
}
