using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Extensions;
using TeamConnect.Models.Team;
using TeamConnect.Services.TeamService;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels.Popups
{
    public class CreateTeamPopupPageViewModel : BaseViewModel
    {
        private readonly ITeamService _teamService;

        public CreateTeamPopupPageViewModel(
            INavigationService navigationService,
            ITeamService teamService)
            : base(navigationService)
        {
            _teamService = teamService;
        }

        #region -- Public properties --

        private string _teamName;
        public string TeamName
        {
            get => _teamName;
            set => SetProperty(ref _teamName, value);
        }

        private ICommand _cancelTapCommand;
        public ICommand CancelTapCommand => _cancelTapCommand ??= new AsyncCommand(OnCancelTapCommandAsync);

        private ICommand _createTapCommand;
        public ICommand CreateTapCommand => _createTapCommand ??= new AsyncCommand(OnCreateTapCommandAsync);

        #endregion

        #region -- Private helpers --

        private Task OnCancelTapCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }

        private async Task OnCreateTapCommandAsync()
        {
            var team = new TeamViewModel
            {
                Name = TeamName,
            };

            await _teamService.CreateTeamAsync(team.ToModel());

            await NavigationService.GoBackAsync();
        }

        #endregion
    }
}
