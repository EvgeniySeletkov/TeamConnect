using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Services.AuthorizationService;

namespace TeamConnect.ViewModels
{
    public class TeamPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;

        public TeamPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

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

            var getAllUsersResult = await _authorizationService.GetAllUsersAsync();

            if (getAllUsersResult.IsSuccess)
            {
                Users = getAllUsersResult.Result.Select(u => u.ToViewModel()).ToList();
            }
        }

        #endregion
    }
}
