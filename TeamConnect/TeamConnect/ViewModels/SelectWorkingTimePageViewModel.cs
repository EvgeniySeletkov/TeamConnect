using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels
{
    public class SelectWorkingTimePageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        private UserViewModel _user;

        public SelectWorkingTimePageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
        }

        #region -- Public properties --

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
        }

        #endregion

        #region -- Private helpers --

        private async Task OnSelectStartWorkingTimeCommandAsync()
        {
            var timePromptResult = await _userDialogs.TimePromptAsync();

            if (timePromptResult.Ok)
            {
                IsStartWorkingTimeChanged = true;
                StartWorkingTime = timePromptResult.SelectedTime;
            }
        }

        private async Task OnSelectEndWorkingTimeCommandAsync()
        {
            var timePromptResult = await _userDialogs.TimePromptAsync();

            if (timePromptResult.Ok)
            {
                IsEndWorkingTimeChanged = true;
                EndWorkingTime = timePromptResult.SelectedTime;
            }
        }

        private Task OnCompleteRegistrationTapCommandAsync()
        {
            _user.StartWorkTime = DateTime.Now.Date + StartWorkingTime;
            _user.EndWorkTime = DateTime.Now + EndWorkingTime;

            return Task.CompletedTask;
        }

        #endregion
    }
}
