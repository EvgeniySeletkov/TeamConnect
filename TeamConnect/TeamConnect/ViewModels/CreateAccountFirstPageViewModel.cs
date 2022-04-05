using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels
{
    public class CreateAccountFirstPageViewModel : BaseViewModel
    {
        public CreateAccountFirstPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private ICommand _nextTapCommand;
        public ICommand NextTapCommand => _nextTapCommand ??= new AsyncCommand(OnNextTapCommandAsync);

        #endregion

        #region -- Private helpers --

        private Task OnNextTapCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(CreateAccountSecondPage), null, false, true);
        }

        #endregion
    }
}
