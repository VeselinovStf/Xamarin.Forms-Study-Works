using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Geolocator.Abstractions;
using SalesApp.Models;
using SalesApp.Services.Address;
using SalesApp.Services.Settings;
using SalesApp.Services.Zones;
using SalesApp.ViewModels.Base;
using Xamarin.Forms;

namespace SalesApp.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private readonly IZoneService _zoneService;
        private readonly IAddressService _addressService;

        public MapViewModel(IZoneService zoneService, IAddressService addressService)
        {
            _zoneService = zoneService;
            _addressService = addressService;
        }

        public override async Task InitializeAsync(object navigationData)
        {

        }
    }
}

