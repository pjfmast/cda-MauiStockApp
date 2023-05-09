namespace MauiStockApp.Models;

public class StockCount
{
    public int Id { get; set; }

    public required string CountedById { get; set; }

    public int ProductId { get; set; }

    public required Product Product { get; set; }

    public DateTime CountedAt { get; set; }

    public required int Count { get; set; }
}