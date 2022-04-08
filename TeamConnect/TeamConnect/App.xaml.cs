using Acr.UserDialogs;
using Prism.Ioc;
using Prism.Unity;
using System.Globalization;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.MapService;
using TeamConnect.Services.MockDataService;
using TeamConnect.Services.RequestService;
using TeamConnect.Services.RestService;
using TeamConnect.Services.TimeZoneService;
using TeamConnect.Services.UserService;
using TeamConnect.ViewModels;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.Helpers;
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

            InitializeLocalizationManager();

            NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Packages
            containerRegistry.RegisterInstance(UserDialogs.Instance);

            // Services
            containerRegistry.RegisterInstance<IMockDataService>(Container.Resolve<MockDataService>());
            containerRegistry.RegisterInstance<IRestService>(Container.Resolve<RestService>());
            containerRegistry.RegisterInstance<ITimeZoneService>(Container.Resolve<TimeZoneService>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IRequestService>(Container.Resolve<RequestService>());
            containerRegistry.RegisterInstance<IMapService>(Container.Resolve<MapService>());

            // Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateAccountFirstPage, CreateAccountFirstPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateAccountSecondPage, CreateAccountSecondPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectLocationPage, SelectLocationPageViewModel>();
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

        #region -- Private helpers --

        private void InitializeLocalizationManager()
        {
            var enCulture = new CultureInfo("en");

            CultureInfo.DefaultThreadCurrentCulture = enCulture;
            CultureInfo.DefaultThreadCurrentUICulture = enCulture;
            CultureInfo.CurrentCulture = enCulture;
            CultureInfo.CurrentUICulture = enCulture;

            LocalizationResourceManager.Current.PropertyChanged += (sender, e) => Strings.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(Strings.ResourceManager);
            LocalizationResourceManager.Current.CurrentCulture = enCulture;
        }

        #endregion
    }
}
