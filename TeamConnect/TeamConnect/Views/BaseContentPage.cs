using Prism.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TeamConnect.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            // Test
            ViewModelLocator.SetAutowireViewModel(this, true);
            On<iOS>().SetUseSafeArea(true);
        }
    }
}