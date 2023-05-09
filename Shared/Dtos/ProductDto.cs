namespace Shared.Dtos;

public class ProductDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string ManufacturerName { get; set; }

    public required string ManufacturerId { get; set; }
}
