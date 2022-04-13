using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
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

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value);
        }

        private ICommand _nextTapCommand;
        public ICommand NextTapCommand => _nextTapCommand ??= new AsyncCommand(OnNextTapCommandAsync);

        #endregion

        #region -- Private helpers --

        private Task OnNextTapCommandAsync()
        {
            var user = new UserViewModel
            {
                Name = Name,
                Surname = Surname,
            };

            var parameters = new NavigationParameters
            {
                { Constants.Navigation.USER,  user.ToModel() },
            };

            return NavigationService.NavigateAsync(nameof(CreateAccountSecondPage), parameters, false, true);
        }

        #endregion
    }
}
