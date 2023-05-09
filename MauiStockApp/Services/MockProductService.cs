using MauiStockApp.Helpers;
using Shared.Dtos;

namespace MauiStockApp.Services;

public class MockProductService : IProductService
{
    private readonly List<ProductDto> products;

    public MockProductService() { 
        products = new List<ProductDto>()
        {
            new ProductDto() {Id=1, ManufacturerId = "1", ManufacturerName="Tabo Surf Shop", Name="Mad Longboard"},
            new ProductDto() {Id=2, ManufacturerId = "1", ManufacturerName="Tabo Surf Shop", Name="Mad Shortboard"},
            new ProductDto() {Id=3, ManufacturerId = "4", ManufacturerName="Jordan'Surf & Kitezoo", Name="Sail small"},
            new ProductDto() {Id=4, ManufacturerId = "4", ManufacturerName="Jordan'Surf & Kitezoo", Name="Sail large"},
            new ProductDto() {Id=5, ManufacturerId = "2", ManufacturerName="Tout Sab Surf Shop", Name="Leggy Leash"},
            new ProductDto() {Id=6, ManufacturerId = "2", ManufacturerName="Tout Sab Surf Shop", Name="SoyBoy Board Wax"},
            new ProductDto() {Id=7, ManufacturerId = "3", ManufacturerName="Bomboklat Surf School", Name="Men's rashie"},
            new ProductDto() {Id=8, ManufacturerId = "3", ManufacturerName="Bomboklat Surf School", Name="Woman's rashie"},
            new ProductDto() {Id=9, ManufacturerId = "3", ManufacturerName="Bomboklat Surf School", Name="Fin pack"},
        };
    }
    public Task<List<ProductDto>> SearchProducts(string searchTerm)
    => Task.FromResult(products.FindAll(p => p.Name.ContainsCaseInsensitive(searchTerm)));

    
}
