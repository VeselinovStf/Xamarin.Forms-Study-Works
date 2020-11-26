using CurrencyListApp.Models;
using CurrencyListApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CurrencyListApp.ViewModels
{
    public class CurrencyExchangeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RecalcCommand { get; private set; }

        private int _SelectedCurrencyIndex;
        public int SelectedCurrencyIndex
        {
            get { return _SelectedCurrencyIndex; }
            set
            {
                if (_SelectedCurrencyIndex != value)
                {
                    _SelectedCurrencyIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _curValue = 10.0;

        public double CurValue
        {
            set
            {
                if (_curValue != value)
                {
                    _curValue = value;
                    OnPropertyChanged();
                }
            }
            get { return _curValue; }
        }

        private CurrencyName _selectedExchangeCurrency;
        public CurrencyName SelectedCurrency
        {
            get { return _selectedExchangeCurrency; }
            set
            {
                if (_selectedExchangeCurrency != value)
                {
                    _selectedExchangeCurrency = value;
                    OnPropertyChanged();
                    Recalc();
                }
            }
        }

        private List<CurrencyName> _CurList;
        public List<CurrencyName> CurList
        {
            get { return _CurList; }
            set
            {
                if (_CurList != value)
                {
                    _CurList = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ExchangeData> CurrencyData { get; } = new ObservableCollection<ExchangeData>();

        public ExchangeRate ExchangeRate { get; private set; }

        private DataService dataService;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CurrencyExchangeViewModel()
        {
            _SelectedCurrencyIndex = -1;
            dataService = new DataService();

            var currentLocale = System.Threading.Thread.CurrentThread.CurrentUICulture;

            CurList = dataService.GetExchangeCurrencies(currentLocale.Name).OrderBy(s => s.Name).ToList();

            RecalcCommand = new Command(async () => await RecalcAsync(), canExecute: () => { return true; });
        }

        async Task RecalcAsync()
        {
            await Task.Run(() => Recalc());
        }

        public void Recalc()
        {
            double thisRate = 1.0;

            if (_selectedExchangeCurrency != null)
            {
                var rate = CurrencyData
                            .FirstOrDefault(c => c.CurrencyCode
                                                    .Equals(_selectedExchangeCurrency.Code,
                                                    StringComparison.CurrentCultureIgnoreCase));

                if (rate != null)
                {
                    thisRate = rate.UsdRate;
                }
            }

            foreach (var ce in CurrencyData)
            {
                ce.CurrencyRate = ce.UsdRate / thisRate;
                ce.CurrencyValue = _curValue * ce.UsdRate / thisRate;
            }
        }

        public async Task InitData()
        {
            ExchangeRate = await dataService.GetExchangeRate();

            List<ExchangeData> data = new List<ExchangeData>();

            List<CurrencySymbol> symbols = dataService.GetCurrencySymbols();

            foreach (var r in ExchangeRate.Rates)
            {
                var newData = new ExchangeData
                {
                    CurrencyCode = r.Key,
                    CurrencySymbol = symbols.FirstOrDefault(s => s.Code.Equals(r.Key)).Symbol,
                    CurrencyName = CurList.FirstOrDefault(s => s.Code.Equals(r.Key)).Name,
                    UsdRate = r.Value
                };

                data.Add(newData);
            }
            // Return the exchange rates in country order
            data = data.OrderBy(d => d.CurrencyName).ToList();

            // performance hit, but quick enough for the demo
            foreach (var d in data)
            {
                CurrencyData.Add(d);
            }

            // Set the picker to US Dollar
            SelectedCurrencyIndex = _CurList.FindIndex(s => s.Code.Equals("USD"));
        }
    }
}
