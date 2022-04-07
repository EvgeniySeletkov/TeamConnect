using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TeamConnect.Controls;
using TeamConnect.Droid.Renderers;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace TeamConnect.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context)
            : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetPadding(0, 0, 0, 0);
                Control.Background = null;
            }
        }

        #endregion
    }
}
