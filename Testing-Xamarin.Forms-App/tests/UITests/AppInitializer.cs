using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            //if (platform == Platform.Android)
            //{
                return ConfigureApp.Android
                    .EnableLocalScreenshots()
                    .ApkFile(@"D:\HUB\Xamarin.Forms-Study-Works\Testing-Xamarin.Forms-App\src\Wired_Brain_Coffee.Android\bin\Debug\com.ps.wb.wired_brain_coffee.apk")
                    .StartApp();
            //}

            //return ConfigureApp.iOS
            //    .EnableLocalScreenshots()
            //    .StartApp();
        }
    }
}