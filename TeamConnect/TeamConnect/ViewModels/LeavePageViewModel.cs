using Acr.UserDialogs;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Enums;
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
        private readonly IUserDialogs _userDialogs;
        private readonly ILeaveService _leaveService;
        private readonly IUserService _userService;

        public LeavePageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            ILeaveService leaveService,
            IUserService userService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _leaveService = leaveService;
            _userService = userService;
        }

        #region -- Public properties --

        private EPageState _pageState;
        public EPageState PageState
        {
            get => _pageState;
            set => SetProperty(ref _pageState, value);
        }

        private List<LeaveGroupViewModel> _leaves;
        public List<LeaveGroupViewModel> Leaves
        {
            get => _leaves;
            set => SetProperty(ref _leaves, value);
        }

        private ICommand _selectDateTapCommand;
        public ICommand SelectDateTapCommand => _selectDateTapCommand ??= new AsyncCommand(OnSelectDateTapCommandAsync);

        private ICommand _resetDateTapCommand;
        public ICommand ResetDateTapCommand => _resetDateTapCommand ??= new AsyncCommand(OnResetDateTapCommandAsync);

        private ICommand _addLeaveTapCommand;
        public ICommand AddLeaveTapCommand => _addLeaveTapCommand ??= new AsyncCommand(OnAddLeaveTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await LoadLeavesAsync();
        }

        #endregion

        #region -- Private helpers --

        private async Task OnResetDateTapCommandAsync()
        {
            await LoadLeavesAsync();
        }

        private async Task LoadLeavesAsync()
        {
            PageState = EPageState.Loading;

            var getLeavesResult = await _leaveService.GetLeavesByDatesAsync(DateTime.Now.Date, DateTime.Now.AddDays(6).Date);

            if (getLeavesResult.IsSuccess)
            {
                var leaves = getLeavesResult.Result;

                var leavesGroups = new List<LeaveGroupViewModel>();

                for (int i = 0; i < 7; i++)
                {
                    leavesGroups.Add(new LeaveGroupViewModel(DateTime.Now.AddDays(i).Date));
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

                PageState = EPageState.Complete;
            }
        }

        private async Task OnSelectDateTapCommandAsync()
        {
            var config = new DatePromptConfig();
            config.iOSPickerStyle = iOSPickerStyle.Wheels;

            var datePromptResult = await _userDialogs.DatePromptAsync(config);

            if (datePromptResult.Ok)
            {
                await LoadLeavesByDateAsync(datePromptResult.SelectedDate);
            }
        }

        private async Task LoadLeavesByDateAsync(DateTime date)
        {
            PageState = EPageState.Loading;

            var leavesGroups = new List<LeaveGroupViewModel>();

            leavesGroups.Add(new LeaveGroupViewModel(date));

            var getLeavesResult = await _leaveService.GetLeavesByDatesAsync(date, date);

            if (getLeavesResult.IsSuccess)
            {
                var leaves = getLeavesResult.Result;

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
                PageState = EPageState.Complete;
            }
            else
            {
                Leaves = leavesGroups;
                PageState = EPageState.NoResult;
            }
        }

        private Task OnAddLeaveTapCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(NewRequestPage), null, false, true);
        }

        #endregion
    }
}
