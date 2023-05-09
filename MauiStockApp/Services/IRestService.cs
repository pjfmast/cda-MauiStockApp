using Shared.Dtos;

namespace MauiStockApp.Services
{
    public interface IRestService
    {
        public /*async*/ Task<List<ProductDto>> SearchProducts(string searchterm);


        public /*async*/ Task<ProductDto> Get(string code);
       
    }
}
