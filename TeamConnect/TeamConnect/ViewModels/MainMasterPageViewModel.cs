using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using TeamConnect.Services.AuthorizationService;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TeamConnect.ViewModels
{
    public class MainMasterPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;

        public MainMasterPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private bool _isPresented;
        public bool IsPresented
        {
            get => _isPresented;
            set => SetProperty(ref _isPresented, value);
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

        #region -- Private helpers --

        private Task OnTeamTapCommandAsync()
        {
            IsPresented = false;

            return NavigationService.NavigateAsync($"/{nameof(MainMasterPage)}/{nameof(NavigationPage)}/{nameof(TeamPage)}", null, false, true);
        }

        private Task OnLeaveTapCommandAsync()
        {
            IsPresented = false;

            return NavigationService.NavigateAsync($"/{nameof(MainMasterPage)}/{nameof(NavigationPage)}/{nameof(LeavePage)}", null, false, true);
        }

        private Task OnTeamTimeTapCommandAsync()
        {
            IsPresented = false;

            return NavigationService.NavigateAsync($"/{nameof(MainMasterPage)}/{nameof(NavigationPage)}/{nameof(TeamTimePage)}", null, false, true);
        }

        private Task OnLogOutTapCommandAsync()
        {
            _authorizationService.LogOut();

            IsPresented = false;

            return NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}", null, false, true);
        }

        #endregion
    }
}
