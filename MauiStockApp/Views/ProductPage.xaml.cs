using MauiStockApp.Models;

namespace MauiStockApp.Views;

[QueryProperty(nameof(Product), nameof(Product))]
public partial class ProductPage : ContentPage
{
    public ProductPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    Product _product;
    public Product Product
    {
        get { return _product; }
        set
        {
            _product = value;
            ProductName = _product.Name;
            ManufacturerName = _product.ManufacturerName;
        }
    }

    string _productName;
    public string ProductName
    {
        get => _productName;
        set
        {
            _productName = value;
            OnPropertyChanged();
        }
    }

    string _manufacturerName;
    public string ManufacturerName
    {
        get => _manufacturerName;
        set
        {
            _manufacturerName = value;
            OnPropertyChanged();
        }
    }
}