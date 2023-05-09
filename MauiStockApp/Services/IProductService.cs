using Shared.Dtos;

namespace MauiStockApp.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> SearchProducts(string searchTerm);
    }
}