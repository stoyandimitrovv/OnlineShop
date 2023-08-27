using ShoppingCart.Models;

namespace ShoppingCart.Services.Contracts
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsForAdmin(int p, int pageSize);

        Task Create(Product product);
        Task Update(Product product);
        Task Delete(long id);
        Task RateProduct(long id, long rating, string idUser);
    }
}
