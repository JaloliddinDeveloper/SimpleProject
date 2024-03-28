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
    }
}
