using RetailApp.Models.Mongo;

namespace RetailApp.Data.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
    }
}
