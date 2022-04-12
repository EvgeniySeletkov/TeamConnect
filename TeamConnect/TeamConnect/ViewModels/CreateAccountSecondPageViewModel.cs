using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Extensions;
using TeamConnect.Helpers;
using TeamConnect.Models.User;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.AuthorizationService;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TeamConnect.ViewModels
{
    public class CreateAccountSecondPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;

        private UserViewModel _user;

        public CreateAccountSecondPageViewModel(
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

        private string _emailError;
        public string EmailError
        {
            get => _emailError;
            set => SetProperty(ref _emailError, value);
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

        private string _passwordError;
        public string PasswordError
        {
            get => _passwordError;
            set => SetProperty(ref _passwordError, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private bool _isConfirmPasswordError;
        public bool IsConfirmPasswordError
        {
            get => _isConfirmPasswordError;
            set => SetProperty(ref _isConfirmPasswordError, value);
        }

        private string _confirmPasswordError;
        public string ConfirmPasswordError
        {
            get => _confirmPasswordError;
            set => SetProperty(ref _confirmPasswordError, value);
        }

        private ICommand _createAccountTapCommand;
        public ICommand CreateAccountTapCommand => _createAccountTapCommand ??= new AsyncCommand(OnCreateAccountTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.TryGetValue(Constants.Navigation.USER, out UserViewModel user))
            {
                _user = user;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnCreateAccountTapCommandAsync()
        {
            if (CheckIsEmailValid())
            {
                if (CheckIsPasswordValid())
                {
                    var checkIsEmailExistResult = await _authorizationService.CheckIsEmailExistAsync(Email);

                    if (!checkIsEmailExistResult.IsSuccess)
                    {
                        _user.Email = Email;
                        _user.Password = Password;

                        var signUpResult = await _authorizationService.SignUpAsync(_user.ToModel());

                        if (signUpResult.IsSuccess)
                        {
                            var parameters = new NavigationParameters
                            {
                                { Constants.Navigation.EMAIL, Email },
                            };

                            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}", parameters, false, true);
                        }
                    }
                    else
                    {
                        IsEmailError = true;
                        EmailError = Strings.PleaseTypeYourEmail;
                    }
                }
            }
        }

        private bool CheckIsEmailValid()
        {
            bool result = false;

            if (Validator.CheckIsEmailValid(Email))
            {
                IsEmailError = false;
                result = true;
            }
            else
            {
                IsEmailError = true;
                EmailError = Strings.WrongEmail;
            }

            return result;
        }

        private bool CheckIsPasswordValid()
        {
            bool result = false;

            if (Validator.CheckIsPasswordValid(Password))
            {
                IsPasswordError = false;

                if (ConfirmPassword == Password)
                {
                    IsConfirmPasswordError = false;
                    result = true;
                }
                else
                {
                    IsConfirmPasswordError = true;
                    ConfirmPasswordError = Strings.PleaseConfirmPassword;
                }
            }
            else
            {
                IsPasswordError = true;
                PasswordError = Strings.WrongPassword;
            }

            return result;
        }

        #endregion
    }
}
