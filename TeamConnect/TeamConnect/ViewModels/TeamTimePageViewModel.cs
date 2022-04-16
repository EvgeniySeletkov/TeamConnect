using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Services.UserService;

namespace TeamConnect.ViewModels
{
    public class TeamTimePageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public TeamTimePageViewModel(
            INavigationService navigationService,
            IUserService userService)
            : base(navigationService)
        {
            _userService = userService;
        }

        #region -- Public properties --

        private List<UserGroupViewModel> _teamTimes;
        public List<UserGroupViewModel> TeamTimes
        {
            get => _teamTimes;
            set => SetProperty(ref _teamTimes, value);
        }

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            await LoadTeamTimes();
        }

        #endregion

        #region -- Private helpers --

        private async Task LoadTeamTimes()
        {
            var getUsersResult = await _userService.GetNotMissingUsersAsync(new DateTime(2022, 5, 30).Date);

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

                TeamTimes = teamTimeGroups;
            }
        }

        private UserGroupViewModel GetTeamTimeGroup(UserViewModel user1, UserViewModel user2)
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
