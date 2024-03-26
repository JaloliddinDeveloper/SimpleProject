using SimpleProject.Api.Brokers.Loggings;
using SimpleProject.Api.Brokers.Storages;
using SimpleProject.Api.Models.Foundations.Products;

namespace SimpleProject.Api.Services.Foundations.Products
{
    public class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ProductService(IStorageBroker storageBroker,ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }
        public async ValueTask<Product> AddProductAsync(Product product)
        {
           return await this.storageBroker.InsertProductAsync(product);
        }
        public IQueryable<Product> RetrieveAllProducts()
        {
           return this.storageBroker.SelectAllProducts();
        }

        public async ValueTask<Product> RetrieveProductByIdAsync(Guid productId)
        {
            return await this.storageBroker.SelectProductByIdAsync(productId);
        }

        public async ValueTask<Product> ModifyProductAsync(Product product)
        {
           return await storageBroker.UpdateProductAsync(product);
        }

        public async ValueTask<Product> RemoveProductByIdAsync(Guid productId)
        {
           Product gettingProduct =
                await this.storageBroker.SelectProductByIdAsync(productId);

            return await this.storageBroker.DeleteProductAsync(gettingProduct);
        }
    }
}
