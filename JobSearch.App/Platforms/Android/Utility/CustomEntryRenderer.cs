using Android.Content;
using JobSearch.App.Platforms.Android.Utility;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace JobSearch.App.Platforms.Android.Utility
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = null;
                //Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}
