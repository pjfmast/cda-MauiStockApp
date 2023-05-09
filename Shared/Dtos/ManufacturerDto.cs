namespace Shared.Dtos;

public class ManufacturerDto
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
