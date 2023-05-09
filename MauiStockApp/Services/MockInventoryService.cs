using Shared.Dtos;

namespace MauiStockApp.Services;

public class MockInventoryService : IInventoryService
{
    private readonly List<InventoryItemDto> _stock;

    public MockInventoryService()
    {
        _stock = new List<InventoryItemDto>()
        {
            new InventoryItemDto() { Id = 1,Count=4,CountedAt=new DateTime(2023,5,8) ,ProductName = "Sail medium", CountedById="2", CountedByName="Henk", ManufacturerName="SurfPro"},
            new InventoryItemDto() { Id = 2,Count=2, CountedAt=new DateTime(2023,5,8), ProductName = "Sail small", CountedById="2", CountedByName="Henk", ManufacturerName="SurfPro"},
        };
    }
    public Task<bool> AddStockCount(ProductDto product, int count)
    {
        InventoryItemDto item = new() {
            CountedAt = DateTime.Today,
            Count = count,
            ProductId = product.Id,
            ProductName = product.Name,
            ManufacturerName = product.ManufacturerName,
            CountedById = "",
            CountedByName = Preferences.Get("UserName", "Unknown")};
        _stock.Add(item);
        return Task.FromResult(true);
    }

    public Task<List<InventoryItemDto>> GetInventory()
    {
        Task.Delay(1000).Wait();

        return Task.FromResult(_stock);
    }
}
