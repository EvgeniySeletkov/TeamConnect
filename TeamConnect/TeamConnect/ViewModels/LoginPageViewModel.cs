using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private ICommand _logInTapCommand;
        public ICommand LogInTapCommand => _logInTapCommand ??= new AsyncCommand(OnLogInTappedAsync);

        private ICommand _signUpTapCommand;
        public ICommand SignUpTapCommand => _signUpTapCommand ??= new AsyncCommand(OnSignUpTappedAsync);

        #endregion

        #region -- Private helpers --

        private Task OnLogInTappedAsync()
        {
            return NavigationService.NavigateAsync(nameof(SelectLocationPage), null, false, true);
        }

        private Task OnSignUpTappedAsync()
        {
            return NavigationService.NavigateAsync(nameof(CreateAccountFirstPage), null, false, true);
        }

        #endregion
    }
}
