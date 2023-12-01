using CharlesAssesment.Models;

namespace CharlesAssesment.Service
{
    public interface IProductService
    {
        Task<IList<Product>> GetProductsAsync();
    }
}