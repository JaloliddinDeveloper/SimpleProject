using SimpleProject.Api.Models.Foundations.Products;

namespace SimpleProject.Api.Services.Foundations.Products
{
    public interface IProductService
    {
        ValueTask<Product> AddProductAsync(Product product);
    }
}
