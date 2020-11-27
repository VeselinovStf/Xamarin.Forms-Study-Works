using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GBMExtending.Controls
{
    public class CameraPreview : View
    {
        public static readonly BindableProperty CameraProperty =
            BindableProperty.Create(
                propertyName: "Camera",
                returnType: typeof(CameraOptions),
                declaringType: typeof(CameraPreview),
                defaultValue: CameraOptions.Rear);

        public CameraOptions Camera
        {
            get => (CameraOptions)GetValue(CameraProperty);
            set => SetValue(CameraProperty, value);
        }
    }
}
