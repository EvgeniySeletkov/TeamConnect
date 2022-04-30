using Acr.UserDialogs;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Enums;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Services.UserService;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels
{
    public class TeamTimePageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IUserService _userService;

        public TeamTimePageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            IUserService userService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _userService = userService;
        }

        #region -- Public properties --

        private EPageState _pageState;
        public EPageState PageState
        {
            get => _pageState;
            set => SetProperty(ref _pageState, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private List<UserGroupViewModel> _teamTimes;
        public List<UserGroupViewModel> TeamTimes
        {
            get => _teamTimes;
            set => SetProperty(ref _teamTimes, value);
        }

        private ICommand _selectDateTapCommand;
        public ICommand SelectDateTapCommand => _selectDateTapCommand ??= new AsyncCommand(OnSelectDateTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            await LoadTeamTimesAsync(DateTime.Now.Date);
        }

        #endregion

        #region -- Private helpers --

        private async Task LoadTeamTimesAsync(DateTime date)
        {
            Date = date;

            PageState = EPageState.Loading;

            var getUsersResult = await _userService.GetNotMissingUsersAsync(Date);

            if (getUsersResult.IsSuccess)
            {
                var users = getUsersResult.Result.Select(u => u.ToViewModel()).ToList();

                var teamTimeGroups = new List<UserGroupViewModel>();

                for (int i = 0; i < users.Count; i++)
                {
                    for (int j = i + 1; j < users.Count; j++)
                    {
                        var group = GetTeamTimeGroup(users[i], users[j]);

                        if (group is not null
                            && group.StartWorkTime != group.EndWorkTime
                            && teamTimeGroups.FirstOrDefault(
                                t => t.StartWorkTime == group.StartWorkTime
                                && t.EndWorkTime == group.EndWorkTime) is null)
                        {
                            teamTimeGroups.Add(group);
                        }
                    }
                }

                foreach (var user in users)
                {
                    foreach (var group in teamTimeGroups)
                    {
                        if (user.StartWorkTime <= group.StartWorkTime
                            && user.EndWorkTime >= group.EndWorkTime)
                        {
                            group.Add(user);
                        }
                    }
                }

                if (teamTimeGroups.Count > 0)
                {
                    TeamTimes = teamTimeGroups;
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

        private UserGroupViewModel GetTeamTimeGroup(UserViewModel user1, UserViewModel user2)
        {
            UserGroupViewModel result = null;

            if (user1.StartWorkTime >= user2.StartWorkTime
                && user2.EndWorkTime <= user1.EndWorkTime)
            {
                if (user1.StartWorkTime < user2.EndWorkTime)
                {
                    result = new UserGroupViewModel(user1.StartWorkTime, user2.EndWorkTime);
                }
            }
            else if (user2.StartWorkTime >= user1.StartWorkTime
                && user1.EndWorkTime <= user2.EndWorkTime)
            {
                if (user2.StartWorkTime < user1.EndWorkTime)
                {
                    result = new UserGroupViewModel(user2.StartWorkTime, user1.EndWorkTime);
                }
            }
            else if (user1.StartWorkTime >= user2.StartWorkTime
                && user1.EndWorkTime <= user2.EndWorkTime)
            {
                if (user1.StartWorkTime < user1.EndWorkTime)
                {
                    result = new UserGroupViewModel(user1.StartWorkTime, user1.EndWorkTime);
                }
            }
            else if (user2.StartWorkTime >= user1.StartWorkTime
                && user2.EndWorkTime <= user1.EndWorkTime)
            {
                if (user2.StartWorkTime < user2.EndWorkTime)
                {
                    result = new UserGroupViewModel(user2.StartWorkTime, user2.EndWorkTime);
                }
            }

            return result;
        }

        private async Task OnSelectDateTapCommandAsync()
        {
            var config = new DatePromptConfig();
            config.iOSPickerStyle = iOSPickerStyle.Wheels;

            var datePromptResult = await _userDialogs.DatePromptAsync(config);

            if (datePromptResult.Ok)
            {
                await LoadTeamTimesAsync(datePromptResult.SelectedDate);
            }
        }

        #endregion
    }
}
