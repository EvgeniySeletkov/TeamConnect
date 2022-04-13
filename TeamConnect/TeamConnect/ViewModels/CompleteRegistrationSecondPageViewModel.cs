using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Resources.Strings;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels
{
    public class CompleteRegistrationSecondPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        private UserViewModel _user;

        public CompleteRegistrationSecondPageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
        }

        #region -- Public properties --

        private List<string> _positions;
        public List<string> Positions
        {
            get => _positions;
            set => SetProperty(ref _positions, value);
        }

        private string _selectedPosition;
        public string SelectedPosition
        {
            get => _selectedPosition;
            set => SetProperty(ref _selectedPosition, value);
        }

        private bool _isStartWorkingTimeChanged;
        public bool IsStartWorkingTimeChanged
        {
            get => _isStartWorkingTimeChanged;
            set => SetProperty(ref _isStartWorkingTimeChanged, value);
        }

        private TimeSpan _startWorkingTime;
        public TimeSpan StartWorkingTime
        {
            get => _startWorkingTime;
            set => SetProperty(ref _startWorkingTime, value);
        }

        private bool _isEndWorkingTimeChanged;
        public bool IsEndWorkingTimeChanged
        {
            get => _isEndWorkingTimeChanged;
            set => SetProperty(ref _isEndWorkingTimeChanged, value);
        }

        private TimeSpan _endWorkingTime;
        public TimeSpan EndWorkingTime
        {
            get => _endWorkingTime;
            set => SetProperty(ref _endWorkingTime, value);
        }

        private ICommand _selectRoleCommand;
        public ICommand SelectRoleCommand => _selectRoleCommand ??= new AsyncCommand(OnSelectRoleCommandAsync);

        private ICommand _selectStartWorkingTimeCommand;
        public ICommand SelectStartWorkingTimeCommand => _selectStartWorkingTimeCommand ??= new AsyncCommand(OnSelectStartWorkingTimeCommandAsync);

        private ICommand _selectEndWorkingTimeCommand;
        public ICommand SelectEndWorkingTimeCommand => _selectEndWorkingTimeCommand ??= new AsyncCommand(OnSelectEndWorkingTimeCommandAsync);

        private ICommand _completeRegistrationTapCommand;
        public ICommand CompleteRegistrationTapCommand => _completeRegistrationTapCommand ??= new AsyncCommand(OnCompleteRegistrationTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.TryGetValue(Constants.Navigation.USER, out UserModel user))
            {
                _user = user.ToViewModel();
            }

            InitializePositions();
        }

        #endregion

        #region -- Private helpers --

        private void InitializePositions()
        {
            Positions = new List<string>
            {
                Strings.HR,
                Strings.ProjectManager,
                Strings.FrontendDeveloper,
                Strings.BackendDeveloper,
                Strings.MobileDeveloper,
                Strings.QA,
                Strings.Designer,
            };
        }

        private async Task OnSelectRoleCommandAsync()
        {
            var selectedPosition = await _userDialogs.ActionSheetAsync(Strings.SelectPosition, Strings.Cancel, null, null, Positions.ToArray());

            if (selectedPosition != Strings.Cancel)
            {
                SelectedPosition = selectedPosition;
            }
        }

        private async Task OnSelectStartWorkingTimeCommandAsync()
        {
            var timePromptResult = await _userDialogs.TimePromptAsync();

            if (timePromptResult.Ok)
            {
                IsStartWorkingTimeChanged = true;
                StartWorkingTime = timePromptResult.SelectedTime;

                if (!IsEndWorkingTimeChanged)
                {
                    IsEndWorkingTimeChanged = true;
                    EndWorkingTime = StartWorkingTime + TimeSpan.FromHours(Constants.DEFAULT_WORKING_TIME);
                }
            }
        }

        private async Task OnSelectEndWorkingTimeCommandAsync()
        {
            var config = new TimePromptConfig();
            config.AndroidStyleId = 1;
            config.iOSPickerStyle = iOSPickerStyle.Wheels;

            var timePromptResult = await _userDialogs.TimePromptAsync();

            if (timePromptResult.Ok)
            {
                IsEndWorkingTimeChanged = true;
                EndWorkingTime = timePromptResult.SelectedTime;

                if (!IsStartWorkingTimeChanged)
                {
                    IsStartWorkingTimeChanged = true;
                    StartWorkingTime = EndWorkingTime - TimeSpan.FromHours(Constants.DEFAULT_WORKING_TIME);
                }
            }
        }

        private Task OnCompleteRegistrationTapCommandAsync()
        {
            _user.Position = SelectedPosition;
            _user.StartWorkTime = DateTime.Now.Date + StartWorkingTime;
            _user.EndWorkTime = DateTime.Now + EndWorkingTime;
            _user.IsAccountCreated = true;

            return Task.CompletedTask;
        }

        #endregion
    }
}
