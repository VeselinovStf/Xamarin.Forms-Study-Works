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
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace SalesApp.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private readonly IZoneService _zoneService;
        private readonly IAddressService _addressService;
        private MapSpan _visibleRegion;


        public MapViewModel(IZoneService zoneService, IAddressService addressService)
        {
            _zoneService = zoneService;
            _addressService = addressService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            //Default Coordinates for sales person
            Coordinates defaultCordinates = _zoneService.GetAssignedLatitudeLongitude();

            //Visible region based on default cordinates + what distance to see
            VisibleRegion = MapSpan.FromCenterAndRadius(
                new Xamarin.Forms.GoogleMaps.Position(defaultCordinates.Latitude, defaultCordinates.Longitude),
                Distance.FromKilometers(1)
                );

            //Move to visible region
            MoveToRegionRequest.MoveToRegion(VisibleRegion);
        }
       
        //Visible area of a map
        public MapSpan VisibleRegion
        {
            get { return _visibleRegion; }
            set 
            {
                _visibleRegion = value;
                RaisePropertyChanged(() => VisibleRegion);
            }
        }

        //Move to visible area - MapSpan
        public MoveToRegionRequest MoveToRegionRequest { get; } = new MoveToRegionRequest();

    }
}

