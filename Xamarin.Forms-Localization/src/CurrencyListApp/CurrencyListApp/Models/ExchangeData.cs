using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CurrencyListApp.Models
{
    public class ExchangeData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }
        public double UsdRate { get; set; }

        // Only need INPC support for the two fields that are being updated

        private double _CurrencyRate;
        public double CurrencyRate
        {
            get { return _CurrencyRate; }
            set
            {
                if (_CurrencyRate != value)
                {
                    _CurrencyRate = value;
                    OnPropertyChanged();
                }
            }
        }


        private double _CurrencyValue;
        public double CurrencyValue
        {
            get { return _CurrencyValue; }
            set
            {
                if (_CurrencyValue != value)
                {
                    _CurrencyValue = value;
                    OnPropertyChanged();
                }
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}