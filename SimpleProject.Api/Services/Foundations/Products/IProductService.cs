using SimpleProject.Api.Models.Foundations.Products;

namespace SimpleProject.Api.Services.Foundations.Products
{
    public interface IProductService
    {
        ValueTask<Product> AddProductAsync(Product Product);
        IQueryable<Product> RetrieveAllProducts();
        ValueTask<Product> RetrieveProductByIdAsync(Guid ProductId);
        ValueTask<Product> ModifyProductAsync(Product Product);
        ValueTask<Product> RemoveProductByIdAsync(Guid ProductId);
    }
}
