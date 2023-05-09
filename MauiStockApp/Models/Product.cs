namespace MauiStockApp.Models;

public class Product
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string ManufacturerName { get; set; }
    // In later chapter:
    //public Manufacturer Manufacturer { get; set; }

    public int ManufacturerId { get; set; }

    // In later chapter:
    public string BarCode { get; set; }

    public List<StockCount> StockCounts { get; set; } = new();
}
