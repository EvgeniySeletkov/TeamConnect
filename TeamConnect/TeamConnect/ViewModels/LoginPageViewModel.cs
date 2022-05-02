using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Extensions;
using TeamConnect.Services.AuthorizationService;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TeamConnect.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;

        public LoginPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private bool _isEmailError;
        public bool IsEmailError
        {
            get => _isEmailError;
            set => SetProperty(ref _isEmailError, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _isPasswordError;
        public bool IsPasswordError
        {
            get => _isPasswordError;
            set => SetProperty(ref _isPasswordError, value);
        }

        private ICommand _logInTapCommand;
        public ICommand LogInTapCommand => _logInTapCommand ??= new AsyncCommand(OnLogInTapCommandAsync);

        private ICommand _signUpTapCommand;
        public ICommand SignUpTapCommand => _signUpTapCommand ??= new AsyncCommand(OnSignUpTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.TryGetValue(Constants.Navigation.EMAIL, out string email))
            {
                Email = email;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnLogInTapCommandAsync()
        {
            var logInResult = await _authorizationService.LogInAsync(Email, Password);

            if (logInResult.IsSuccess)
            {
                IsEmailError = false;
                IsPasswordError = false;

                var user = logInResult.Result.ToViewModel();

                if (user.IsAccountCreated)
                {
                    await NavigationService.NavigateAsync($"/{nameof(MainMasterPage)}/{nameof(NavigationPage)}/{nameof(TeamPage)}", null, false, true);
                }
                else
                {
                    var parameters = new NavigationParameters
                    {
                        { Constants.Navigation.USER, user.ToModel() },
                    };

                    await NavigationService.NavigateAsync(nameof(CompleteRegistrationFirstPage), parameters, false, true);
                }
            }
            else
            {
                IsEmailError = true;
                IsPasswordError = true;
            }
        }

        private Task OnSignUpTapCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(CreateAccountFirstPage), null, false, true);
        }

        #endregion
    }
}
