using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TeamConnect.Controls;
using TeamConnect.iOS.Renderers;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace TeamConnect.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;

                var color = (Color)Xamarin.Forms.Application.Current.Resources["appcolor_i1"];
                Control.TintColor = color.ToUIColor();
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }

        #endregion
    }
}