using Prism.Mvvm;
using Prism.Navigation;

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
        }

        #endregion
    }
}
