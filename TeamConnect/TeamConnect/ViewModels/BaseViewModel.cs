using System.Linq;
using Prism.Mvvm;
using Prism.Navigation;
using TeamConnect.Views;
using Xamarin.Forms;

namespace TeamConnect.ViewModels
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware
    {
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region -- Public properties --

        public INavigationService NavigationService { get; private set; }

        #endregion

        #region -- IInitialize implementation --

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- INavigationAware implementation --

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            if (Device.RuntimePlatform == Device.Android && App.Current.MainPage is MasterDetailPage page)
            {
                page.IsGestureEnabled = page.Detail.Navigation.NavigationStack.LastOrDefault() is BaseDetailPage;
            }
        }

        #endregion
    }
}
