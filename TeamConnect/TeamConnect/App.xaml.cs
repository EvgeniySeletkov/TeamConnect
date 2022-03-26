using Prism.Ioc;
using Prism.Unity;
using TeamConnect.Services.MockDataService;
using TeamConnect.Services.RequestService;
using TeamConnect.Services.UserService;
using TeamConnect.ViewModels;
using TeamConnect.Views;
using Xamarin.Forms;

namespace TeamConnect
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        #region -- Overrides --

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(TeamTimeListPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterInstance<IMockDataService>(Container.Resolve<MockDataService>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IRequestService>(Container.Resolve<RequestService>());

            // Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<RequestsPage, RequestsPageViewModel>();
            containerRegistry.RegisterForNavigation<TeamTimeListPage, TeamTimeListPageViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion
    }
}
