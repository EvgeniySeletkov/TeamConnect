using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using TeamConnect.Enums;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.AuthorizationService;
using TeamConnect.Services.UserService;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TeamConnect.ViewModels
{
    public class MainMasterPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;

        public MainMasterPageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            IUserService userService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _userService = userService;
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private bool _isPresented;
        public bool IsPresented
        {
            get => _isPresented;
            set => SetProperty(ref _isPresented, value);
        }

        private EDetailPage _detailPage;
        public EDetailPage DetailPage
        {
            get => _detailPage;
            set => SetProperty(ref _detailPage, value);
        }

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private ICommand _teamTapCommand;
        public ICommand TeamTapCommand => _teamTapCommand ??= new AsyncCommand(OnTeamTapCommandAsync);

        private ICommand _leaveTapCommand;
        public ICommand LeaveTapCommand => _leaveTapCommand ??= new AsyncCommand(OnLeaveTapCommandAsync);

        private ICommand _teamTimeTapCommand;
        public ICommand TeamTimeTapCommand => _teamTimeTapCommand ??= new AsyncCommand(OnTeamTimeTapCommandAsync);

        private ICommand _logOutTapCommand;
        public ICommand LogOutTapCommand => _logOutTapCommand ??= new AsyncCommand(OnLogOutTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.TryGetValue(Constants.Navigation.DETAIL_PAGE, out EDetailPage detailPage))
            {
                DetailPage = detailPage;
            }

            var getCurrentUserResult = await _userService.GetCurrentUserAsync();

            if (getCurrentUserResult.IsSuccess)
            {
                User = getCurrentUserResult.Result.ToViewModel();
            }
        }

        #endregion

        #region -- Private helpers --

        private Task OnTeamTapCommandAsync()
        {
            IsPresented = false;

            var parameters = new NavigationParameters
            {
                { Constants.Navigation.DETAIL_PAGE, EDetailPage.Team },
            };

            return NavigationService.NavigateAsync($"/{nameof(MainMasterPage)}/{nameof(NavigationPage)}/{nameof(TeamPage)}", parameters, false, true);
        }

        private Task OnLeaveTapCommandAsync()
        {
            IsPresented = false;

            var parameters = new NavigationParameters
            {
                { Constants.Navigation.DETAIL_PAGE, EDetailPage.Leave },
            };

            return NavigationService.NavigateAsync($"/{nameof(MainMasterPage)}/{nameof(NavigationPage)}/{nameof(LeavePage)}", parameters, false, true);
        }

        private Task OnTeamTimeTapCommandAsync()
        {
            IsPresented = false;

            var parameters = new NavigationParameters
            {
                { Constants.Navigation.DETAIL_PAGE, EDetailPage.TeamTime },
            };

            return NavigationService.NavigateAsync($"/{nameof(MainMasterPage)}/{nameof(NavigationPage)}/{nameof(TeamTimePage)}", parameters, false, true);
        }

        private async Task OnLogOutTapCommandAsync()
        {
            var islogOutConfirmed = await _userDialogs.ConfirmAsync(
                Strings.AreYouSureYouWantLogOut,
                Strings.LogOut,
                Strings.Yes,
                Strings.No);

            if (islogOutConfirmed)
            {
                IsPresented = false;

                _authorizationService.LogOut();

                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}", null, false, true);
            }
        }

        #endregion
    }
}
