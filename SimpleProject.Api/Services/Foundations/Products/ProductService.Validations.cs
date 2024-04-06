//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using SimpleProject.Api.Models.Foundations.Products;
using SimpleProject.Api.Models.Foundations.Products.Exceptions;

namespace SimpleProject.Api.Services.Foundations.Products
{
    public partial class ProductService
    {
        private void ValidateProductNotNull(Product product)
        {
            if (product is null)
            {
                throw new NullProductException();
            }
        }
    }
}
