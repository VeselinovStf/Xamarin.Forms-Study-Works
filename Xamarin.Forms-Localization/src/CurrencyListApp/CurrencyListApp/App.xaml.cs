using CurrencyListApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyListApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //ChangeCulture("es-ES");
            //ChangeCulture("zh-CN");

            MainPage = new CurView();
        }

        private void ChangeCulture(string locale)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(locale);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }

        protected override async void OnStart()
        {
            await ((CurView)MainPage).RefreshData();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
