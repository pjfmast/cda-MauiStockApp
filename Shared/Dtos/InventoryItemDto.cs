namespace Shared.Dtos;

public class InventoryItemDto
{
    public int Id { get; set; }

    // todo: not used (remove?)
    public required string CountedById { get; set; } = string.Empty;

    public required string CountedByName { get; set; }

    public int ProductId { get; set; }

    public required string ProductName { get; set; }

    public required string ManufacturerName { get; set; }

    public DateTime CountedAt { get; set; }

    public int Count { get; set; }
}
