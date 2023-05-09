using MauiStockApp.ViewModels;

namespace MauiStockApp.Views;

public partial class MapPage : ContentPage
{
    private readonly MapViewModel _viewModel;
    public MapPage(MapViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.Init();
        map.MoveToRegion(_viewModel.VisibleArea);
    }
}