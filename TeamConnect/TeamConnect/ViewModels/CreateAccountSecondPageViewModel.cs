using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TeamConnect.ViewModels
{
    public class CreateAccountSecondPageViewModel : BaseViewModel
    {
        public CreateAccountSecondPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private ICommand _createAccountTapCommand;
        public ICommand CreateAccountTapCommand => _createAccountTapCommand ??= new AsyncCommand(OnCreateAccountTapCommandAsync);

        #endregion

        #region -- Private helpers --

        private Task OnCreateAccountTapCommandAsync()
        {
            return NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}", null, false, true);
        }

        #endregion
    }
}
