using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Extensions;
using TeamConnect.Models.Leave;
using TeamConnect.Services.LeaveService;
using TeamConnect.Services.UserService;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels
{
    public class LeavePageViewModel : BaseViewModel
    {
        private readonly ILeaveService _leaveService;
        private readonly IUserService _userService;

        public LeavePageViewModel(
            INavigationService navigationService,
            ILeaveService leaveService,
            IUserService userService)
            : base(navigationService)
        {
            _leaveService = leaveService;
            _userService = userService;
        }

        #region -- Public properties --

        private List<LeaveGroupViewModel> _leaves;
        public List<LeaveGroupViewModel> Leaves
        {
            get => _leaves;
            set => SetProperty(ref _leaves, value);
        }

        private ICommand _addLeaveTapCommand;
        public ICommand AddLeaveTapCommand => _addLeaveTapCommand ??= new AsyncCommand(OnAddLeaveTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            await LoadLeavesAsync();
        }

        #endregion

        #region -- Private helpers --

        private async Task LoadLeavesAsync()
        {
            var getLeavesResult = await _leaveService.GetAllLeavesAsync();

            if (getLeavesResult.IsSuccess)
            {
                var leaves = getLeavesResult.Result;

                var leavesGroups = new List<LeaveGroupViewModel>();

                for (int i = 0; i < 7; i++)
                {
                    leavesGroups.Add(new LeaveGroupViewModel(DateTime.Now.AddDays(i)));
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

        private Task OnAddLeaveTapCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(NewRequestPage), null, false, true);
        }

        #endregion
    }
}
