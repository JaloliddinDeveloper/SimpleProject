//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using SimpleProject.Api.Models.Foundations.Products;
using SimpleProject.Api.Models.Foundations.Products.Exceptions;
using Xeptions;

namespace SimpleProject.Api.Services.Foundations.Products
{
    public partial class ProductService
    {
        private delegate ValueTask<Product> ReturningProductFunction();
        private async ValueTask<Product> TryCatch(ReturningProductFunction returningProductFunction)
        {
            try
            {
                return await returningProductFunction();
            }
            catch (NullProductException nullProductException)
            {
                throw CreateAndLogValidationException(nullProductException);
            }
        }
        private ProductValidationException CreateAndLogValidationException(Xeption exception)
        {
            var productValidationException =
                   new ProductValidationException(exception);
            this.loggingBroker.LogError(productValidationException);

            return productValidationException;
        }
    }
}
