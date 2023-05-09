using Shared.Dtos;

namespace MauiStockApp.Services
{
    public interface IInventoryService
    {
        Task<bool> AddStockCount(ProductDto product, int count);

        Task<List<InventoryItemDto>> GetInventory();
    }
}