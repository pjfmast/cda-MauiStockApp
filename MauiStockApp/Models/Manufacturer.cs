namespace MauiStockApp.Models;

public class Manufacturer
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();
}