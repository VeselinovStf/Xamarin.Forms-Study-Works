using CurrencyListApp.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyListApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurView : ContentPage
	{
        public CurrencyExchangeViewModel ViewModel { get; private set; }
        private CancellationTokenSource throttleCts = new CancellationTokenSource();

        public CurView ()
		{
			InitializeComponent ();

            if (Device.RuntimePlatform == Device.UWP)
            {
                AppTitle.IsVisible = false;
            }

            ViewModel = new CurrencyExchangeViewModel();

            BindingContext = ViewModel;

            CurData.RefreshCommand = new Command(async () =>
            {
                await RefreshData();
                CurData.IsRefreshing = false;
            });

            this.CurValue.TextChanged += CurValueOnTextChanged;

        }
        private void CurValueOnTextChanged(object sender, TextChangedEventArgs e)
        {
            // If the user enters a new digit, reset our delay on processing
            Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();

            // Wait 500ms before updating the list
            Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token)
                .ContinueWith(
                    delegate { this.ViewModel.Recalc(); },
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        public async Task RefreshData()
        {
            await ViewModel.InitData();
        }
    }
}