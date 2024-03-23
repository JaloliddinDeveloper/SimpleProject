//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using Microsoft.EntityFrameworkCore;
using SimpleProject.Api.Models.Foundations.Products;

namespace SimpleProject.Api.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        public DbSet<Product> Products { get; set; }

        public async ValueTask<Product> InsertProductAsync(Product product)
        {
            return await InsertAsync(product);
        }

        public IQueryable<Product> SelectAllProducts()
        {
            return SelectAll<Product>();
        }

        public async ValueTask<Product> SelectProductByIdAsync(Guid productId)
        {
            return await SelectAsync<Product>(productId);
        }

        public async ValueTask<Product> UpdateProductAsync(Product product)
        {
           return await UpdateAsync(product);
        }
        public async ValueTask<Product> DeleteProductAsync(Product product)
        {
            return await DeleteAsync(product);
        }
    }
}
