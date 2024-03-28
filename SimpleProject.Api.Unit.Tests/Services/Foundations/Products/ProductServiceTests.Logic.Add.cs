//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SimpleProject.Api.Models.Foundations.Products;

namespace SimpleProject.Api.Unit.Tests.Services.Foundations.Products
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async Task ShouldAddProductAsync()
        {
            // given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            Product storageProduct = inputProduct;
            Product expectedProduct = storageProduct.DeepClone();

            this.storageBrokerMock.Setup(broker =>
              broker.InsertProductAsync(inputProduct))
                .ReturnsAsync(expectedProduct);

            // when 
            Product actualProduct =
                await this.productService.AddProductAsync(inputProduct);

            // then
            actualProduct.Should().BeEquivalentTo(expectedProduct);

            this.storageBrokerMock.Verify(broker =>
              broker.InsertProductAsync(It.IsAny<Product>()), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
