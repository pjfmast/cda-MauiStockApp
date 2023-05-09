using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStockApp.Services
{
    public class MockManufactorerService : IManufactorerService
    {
        private readonly List<ManufacturerDto> manufacturers;

        public MockManufactorerService()
        {
            manufacturers = new List<ManufacturerDto>()
            {
                new ManufacturerDto() { Id = 1, Name = "Tabo Surf Shop", Latitude=46.170042227262535, Longitude=9.371165007691308},
                new ManufacturerDto() { Id = 2, Name = "Tout Sab Surf Shop", Latitude=46.1694355313993, Longitude=9.37089184648777},
                new ManufacturerDto() { Id = 3, Name = "Bomboklat Surf School", Latitude=46.16391942309194, Longitude=9.379801161229866},
                new ManufacturerDto() { Id = 4, Name = "Jordan'Surf & Kitezoo", Latitude=46.14334511381569, Longitude=9.37507436832627},
            };
        }
        public Task<bool> AddManufactorer(ManufacturerDto manufacturer)
        {
            manufacturers.Add(manufacturer);

            return Task.FromResult(true);
        }

        public Task<List<ManufacturerDto>> GetManufacturers()
         => Task.FromResult(manufacturers);
    }
}
