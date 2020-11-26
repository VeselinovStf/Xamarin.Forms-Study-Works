using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using GBMExtending.Controls;
using GBMExtending.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyCustomEntry), typeof(MyCustomEntryRenderer))]
namespace GBMExtending.iOS
{
    public class MyCustomEntryRenderer : EntryRenderer
    {
      
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Text = "This is my custom iOS ENTRY";
            }
        }
    }
}