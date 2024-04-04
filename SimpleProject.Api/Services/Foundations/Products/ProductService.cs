//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using SimpleProject.Api.Brokers.Loggings;
using SimpleProject.Api.Brokers.Storages;
using SimpleProject.Api.Models.Foundations.Products;
using SimpleProject.Api.Models.Foundations.Products.Exceptions;

namespace SimpleProject.Api.Services.Foundations.Products
{
    public class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ProductService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Product> AddProductAsync(Product product)
        {
            try
            {


                if (product is null)
                {
                    throw new NullProductException();
                }
                return await this.storageBroker.InsertProductAsync(product);
            }
            catch(NullProductException nullProductException)
            {
                var productValidationException =
                    new ProductValidationException(nullProductException);
                this.loggingBroker.LogError(productValidationException);

                throw productValidationException;
            }
        }
    }
}
