using Acr.UserDialogs;
using Prism.Ioc;
using Prism.Unity;
using System.Globalization;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.MapService;
using TeamConnect.Services.LeaveService;
using TeamConnect.Services.RestService;
using TeamConnect.Services.TimeZoneService;
using TeamConnect.Services.AuthorizationService;
using TeamConnect.ViewModels;
using TeamConnect.Views;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using TeamConnect.Services.UserService;
using TeamConnect.Services.SettingsManager;
using TeamConnect.Services.Repository;

namespace TeamConnect
{
    public partial class App : PrismApplication
    {
        private IAuthorizationService _authorizationService;
        private IAuthorizationService AuthorizationService => _authorizationService ??= Container.Resolve<IAuthorizationService>();

        public App()
        {
        }

        #region -- Overrides --

        protected override void OnInitialized()
        {
            InitializeComponent();

            InitializeLocalizationManager();

            if (AuthorizationService.IsAuthorized)
            {
                NavigationService.NavigateAsync($"/{nameof(MainMasterPage)}/{nameof(NavigationPage)}/{nameof(TeamPage)}");
            }
            else
            {
                NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Packages
            containerRegistry.RegisterInstance(UserDialogs.Instance);

            // Services
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRestService>(Container.Resolve<RestService>());
            containerRegistry.RegisterInstance<ITimeZoneService>(Container.Resolve<TimeZoneService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<ILeaveService>(Container.Resolve<LeaveService>());
            containerRegistry.RegisterInstance<IMapService>(Container.Resolve<MapService>());

            // Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateAccountFirstPage, CreateAccountFirstPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateAccountSecondPage, CreateAccountSecondPageViewModel>();
            containerRegistry.RegisterForNavigation<CompleteRegistrationFirstPage, CompleteRegistrationFirstPageViewModel>();
            containerRegistry.RegisterForNavigation<CompleteRegistrationSecondPage, CompleteRegistrationSecondPageViewModel>();
            containerRegistry.RegisterForNavigation<MainMasterPage, MainMasterPageViewModel>();
            containerRegistry.RegisterForNavigation<TeamPage, TeamPageViewModel>();
            containerRegistry.RegisterForNavigation<LeavePage, LeavePageViewModel>();
            containerRegistry.RegisterForNavigation<NewRequestPage, NewRequestPageViewModel>();
            containerRegistry.RegisterForNavigation<TeamTimePage, TeamTimePageViewModel>();
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
