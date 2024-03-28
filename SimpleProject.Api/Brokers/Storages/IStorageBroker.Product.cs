using SimpleProject.Api.Models.Foundations.Products;

namespace SimpleProject.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Product> InsertProductAsync(Product product);
    }
}
