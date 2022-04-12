using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Services.AuthorizationService;

namespace TeamConnect.ViewModels
{
    public class TeamTimeListPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;

        public TeamTimeListPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private List<UserGroupViewModel> _teamAvailableTimes;
        public List<UserGroupViewModel> TeamAvailableTimes
        {
            get => _teamAvailableTimes;
            set => SetProperty(ref _teamAvailableTimes, value);
        }

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            await LoadTeamAvailableTimes();
        }

        #endregion

        #region -- Private helpers --

        private async Task LoadTeamAvailableTimes()
        {
            var getUsersResult = await _authorizationService.GetMissingUsersAsync(DateTime.Now.AddDays(30).Date);

            if (getUsersResult.IsSuccess)
            {
                var users = getUsersResult.Result.Select(u => u.ToViewModel()).ToList();

                var teamAvailableTimeGroups = new List<UserGroupViewModel>();

                for (int i = 0; i < users.Count; i++)
                {
                    for (int j = i + 1; j < users.Count; j++)
                    {
                        var group = GetTeamAvailableTimeGroup(users[i], users[j]);

                        if (group is not null
                            && group.StartWorkTime != group.EndWorkTime
                            && teamAvailableTimeGroups.FirstOrDefault(
                                t => t.StartWorkTime == group.StartWorkTime
                                && t.EndWorkTime == group.EndWorkTime) is null)
                        {
                            teamAvailableTimeGroups.Add(group);
                        }
                    }
                }

                foreach (var user in users)
                {
                    foreach (var group in teamAvailableTimeGroups)
                    {
                        if (user.StartWorkTime <= group.StartWorkTime
                            && user.EndWorkTime >= group.EndWorkTime)
                        {
                            group.Add(user);
                        }
                    }
                }

                TeamAvailableTimes = teamAvailableTimeGroups;
            }
        }

        private UserGroupViewModel GetTeamAvailableTimeGroup(UserViewModel user1, UserViewModel user2)
        {
            UserGroupViewModel result = null;

            if (user1.StartWorkTime >= user2.StartWorkTime
                && user2.EndWorkTime <= user1.EndWorkTime)
            {
                result = new UserGroupViewModel(user1.StartWorkTime, user2.EndWorkTime);
            }
            else if (user2.StartWorkTime >= user1.StartWorkTime
                && user1.EndWorkTime <= user2.EndWorkTime)
            {
                result = new UserGroupViewModel(user2.StartWorkTime, user1.EndWorkTime);
            }
            else if (user1.StartWorkTime >= user2.StartWorkTime
                && user1.EndWorkTime <= user2.EndWorkTime)
            {
                result = new UserGroupViewModel(user1.StartWorkTime, user1.EndWorkTime);
            }
            else if (user2.StartWorkTime >= user1.StartWorkTime
                && user2.EndWorkTime <= user1.EndWorkTime)
            {
                result = new UserGroupViewModel(user2.StartWorkTime, user2.EndWorkTime);
            }

            return result;
        }

        #endregion
    }
}
