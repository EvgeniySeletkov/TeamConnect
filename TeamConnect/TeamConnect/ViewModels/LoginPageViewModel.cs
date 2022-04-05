using Prism.Navigation;
using System;
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

        private ICommand _signUpTapCommand;
        public ICommand SignUpTapCommand => _signUpTapCommand ??= new AsyncCommand(OnSignUpTapCommandAsync);

        #endregion

        #region -- Private helpers --

        private Task OnSignUpTapCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(CreateAccountFirstPage), null, false, true);
        }

        #endregion
    }
}
