using Prism.Ioc;
using Prism.Unity;
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

            NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
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
