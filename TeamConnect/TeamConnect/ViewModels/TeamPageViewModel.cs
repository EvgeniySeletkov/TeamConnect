using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using TeamConnect.Enums;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Services.UserService;

namespace TeamConnect.ViewModels
{
    public class TeamPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public TeamPageViewModel(
            INavigationService navigationService,
            IUserService userService)
            : base(navigationService)
        {
            _userService = userService;
        }

        #region -- Public properties --

        private EPageState _pageState;
        public EPageState PageState
        {
            get => _pageState;
            set => SetProperty(ref _pageState, value);
        }

        private List<UserViewModel> _users;
        public List<UserViewModel> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            var getAllUsersResult = await _userService.GetAllUsersAsync();

            if (getAllUsersResult.IsSuccess)
            {
                Users = getAllUsersResult.Result.Select(u => u.ToViewModel()).ToList();
                PageState = EPageState.Complete;
            }
            else
            {
                PageState = EPageState.NoResult;
            }
        }

        #endregion
    }
}
