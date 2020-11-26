using GBMExtending.Controls;
using GBMExtending.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MyCustomEntry), typeof(MyCustomRenderer))]
namespace GBMExtending.UWP
{
    public class MyCustomRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Text = "This is my custom ENTRY";
            }
        }
    }
}
