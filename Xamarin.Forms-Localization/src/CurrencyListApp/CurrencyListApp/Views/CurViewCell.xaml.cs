using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyListApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurViewCell : ContentView
	{
		public CurViewCell ()
		{
			InitializeComponent ();
		}
	}
}