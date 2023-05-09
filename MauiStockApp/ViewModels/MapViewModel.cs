using MauiStockApp.Models;
using MauiStockApp.Services;
using Microsoft.Maui.Maps;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiStockApp.ViewModels;

public class MapViewModel : BaseViewModel
{
    readonly IManufactorerService _manufactorerService;
    readonly ObservableCollection<Position> _positions;

    public IEnumerable Positions => _positions;

    public ICommand FilterNearbyLocationsCommand { get; }
   

    public MapViewModel(IManufactorerService manufactorerService)
    {
        _positions = new ObservableCollection<Position>();
        _manufactorerService = manufactorerService;

        FilterNearbyLocationsCommand = new Command(NearByLocation);

    }

    private void NearByLocation(object obj)
    {
        throw new NotImplementedException();
    }

    public async Task Init()
    {
        if (initialised)
            return;

        initialised = true;

        await UpdatePositions();
    }

    private MapSpan visibleArea;

    public MapSpan VisibleArea
    {
        get { return visibleArea; }
    }

    private async Task UpdatePositions()
    {
        
        var manufactorers = await _manufactorerService.GetManufacturers();
        foreach ( var manufacturer in manufactorers )
        {
            _positions.Add(new Position("", manufacturer.Name, new Location(manufacturer.Latitude, manufacturer.Longitude)));
        }

        double centreLatitude = _positions.Average(p => p.Location.Latitude);
        double centreLongitude = _positions.Average(p => p.Location.Longitude);
        Location centreArea = new Location(centreLatitude, centreLongitude);
        visibleArea = new MapSpan(centreArea, 0.1, 0.1);
    }


   
}
