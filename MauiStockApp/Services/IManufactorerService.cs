using Shared.Dtos;

namespace MauiStockApp.Services
{
    public interface IManufactorerService
    {
        Task<bool> AddManufactorer(ManufacturerDto manufacturer);

        Task<List<ManufacturerDto>> GetManufacturers();
    }
}