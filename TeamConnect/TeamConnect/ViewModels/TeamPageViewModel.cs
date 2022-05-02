using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using TeamConnect.Enums;
using TeamConnect.Extensions;
using TeamConnect.Models.Team;
using TeamConnect.Models.User;
using TeamConnect.Services.TeamService;
using TeamConnect.Services.UserService;
using TeamConnect.Views.Popups;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels
{
    public class TeamPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public TeamPageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            ITeamService teamService,
            IUserService userService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _teamService = teamService;
            _userService = userService;
        }

        #region -- Public properties --

        private EPageState _pageState;
        public EPageState PageState
        {
            get => _pageState;
            set => SetProperty(ref _pageState, value);
        }

        private TeamViewModel _team;
        public TeamViewModel Team
        {
            get => _team;
            set => SetProperty(ref _team, value);
        }

        private List<UserViewModel> _users;
        public List<UserViewModel> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        private ICommand _createTeamTapCommand;
        public ICommand CreateTeamTapCommand => _createTeamTapCommand ??= new AsyncCommand(OnCreateTeamTapCommandAsync);

        private ICommand _addMemberTapCommand;
        public ICommand AddMemberTapCommand => _addMemberTapCommand ??= new AsyncCommand(OnAddMemberTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(Constants.Navigation.MEMBER_NOT_ADDED, out string addMemberError))
            {
                await _userDialogs.AlertAsync(addMemberError);
            }
            else
            {
                await LoadTeamAsync();
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task LoadTeamAsync()
        {
            var isUserInTeam = _teamService.IsUserInTeam;

            if (isUserInTeam)
            {
                var getTeamResult = await _teamService.GetTeamAsync();

                if (getTeamResult.IsSuccess)
                {
                    var getAllUsersResult = await _userService.GetTeamMembersAsync();

                    if (getAllUsersResult.IsSuccess)
                    {
                        Team = getTeamResult.Result.ToViewModel();
                        Users = getAllUsersResult.Result.Select(u => u.ToViewModel()).ToList();
                        PageState = EPageState.Complete;
                    }
                    else
                    {
                        PageState = EPageState.NoResult;
                    }
                }
                else
                {
                    PageState = EPageState.NoResult;
                }
            }
            else
            {
                PageState = EPageState.TeamNotCreated;
            }
        }

        private Task OnCreateTeamTapCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(CreateTeamPopupPage));
        }

        private Task OnAddMemberTapCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(AddMembersPopupPage));
        }

        #endregion
    }
}
