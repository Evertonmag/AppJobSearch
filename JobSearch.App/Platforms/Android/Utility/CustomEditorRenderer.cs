using Android.Content;
using JobSearch.App.Platforms.Android.Utility;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace JobSearch.App.Platforms.Android.Utility
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
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
