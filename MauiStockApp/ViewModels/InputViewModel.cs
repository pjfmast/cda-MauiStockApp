using MauiStockApp.Services;
using Shared.Dtos;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiStockApp.ViewModels;

public class InputViewModel : BaseViewModel
{
    public string SearchTerm { get; set; }

    private readonly IProductService _productService;
    private readonly IInventoryService _inventoryService;

    public InputViewModel(IProductService productService, IInventoryService inventoryService)
    {
        _productService = productService;
        _inventoryService = inventoryService;
        SearchProductsCommand = new Command<string>(async (term) => await UpdateSearchResults());
        AddCountCommand = new Command<ISearchBar>(async (ISearchBar sb) => await AddCount(sb));
    }
    public ICommand SearchProductsCommand { get; set; }

    public ICommand AddCountCommand { get; set; }

    public ObservableCollection<ProductDto> SearchResults { get; set; } = new();

    private ProductDto _selectedProduct;
    public ProductDto SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            OnPropertyChanged();
        }
    }

    private int _count = 0;

    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            OnPropertyChanged();
        }
    }

    private async Task UpdateSearchResults()
    {
        IsLoading = true;

        SearchResults.Clear();

        var results = await _productService.SearchProducts(SearchTerm);

        IsLoading = false;

        results.ForEach(SearchResults.Add);
    }

    private async Task AddCount(ISearchBar searchBar)
    {
        if (SelectedProduct is null)
        {
            await App.Current.MainPage.DisplayAlert("Product Required", "You have not selected a product to record a count for", "OK");
            return;
        }

        IsLoading = true;

        var added = await _inventoryService.AddStockCount(SelectedProduct, Count);

        IsLoading = false;

        if (added)
        {
            await App.Current.MainPage.DisplayAlert("Added", "Stock count has been added to inventory", "OK");
            ResetForm();
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Error", "Something went  wrong, please try again.", "OK");
        }
    }

    private void ResetForm()
    {
        SearchResults.Clear();
        Count = 0;
        SelectedProduct = null;
        SearchTerm = String.Empty;
        OnPropertyChanged(nameof(SearchTerm));
    }
}