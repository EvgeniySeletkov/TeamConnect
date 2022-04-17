using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Extensions;
using TeamConnect.Models.Request;
using TeamConnect.Services.RequestService;
using TeamConnect.Services.UserService;

namespace TeamConnect.ViewModels
{
    public class LeavePageViewModel : BaseViewModel
    {
        private readonly IRequestService _requestService;
        private readonly IUserService _userService;

        public LeavePageViewModel(
            INavigationService navigationService,
            IRequestService requestService,
            IUserService userService)
            : base(navigationService)
        {
            _requestService = requestService;
            _userService = userService;
        }

        #region -- Public properties --

        private List<RequestGroupViewModel> _leaves;
        public List<RequestGroupViewModel> Leaves
        {
            get => _leaves;
            set => SetProperty(ref _leaves, value);
        }

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            await LoadRequestsAsync();
        }

        #endregion

        #region -- Private helpers --

        private async Task LoadRequestsAsync()
        {
            var getLeavesResult = await _requestService.GetAllRequestsAsync();

            if (getLeavesResult.IsSuccess)
            {
                var leaves = getLeavesResult.Result;

                var leavesGroups = new List<RequestGroupViewModel>();

                for (int i = 0; i < 7; i++)
                {
                    leavesGroups.Add(new RequestGroupViewModel(DateTime.Now.AddDays(i)));
                }

                foreach (var leaveGroup in leavesGroups)
                {
                    foreach (var leave in leaves)
                    {
                        if (leaveGroup.Date >= leave.StartDate && leaveGroup.Date <= leave.EndDate)
                        {
                            var getUserResult = await _userService.GetUserByIdAsync(leave.UserId);

                            if (getUserResult.IsSuccess)
                            {
                                leaveGroup.Add(leave.ToViewModel(getUserResult.Result));
                            }
                        }
                    }
                }

                Leaves = leavesGroups;
            }
        }

        #endregion
    }
}
