//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using Moq;
using SimpleProject.Api.Brokers.Loggings;
using SimpleProject.Api.Brokers.Storages;
using SimpleProject.Api.Models.Foundations.Products;
using SimpleProject.Api.Services.Foundations.Products;
using Tynamix.ObjectFiller;

namespace SimpleProject.Api.Unit.Tests.Services.Foundations.Products
{
    public partial class ProductServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IProductService productService;

        public ProductServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.productService =
                new ProductService(storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }
        private static Product CreateRandomProduct() =>
            CreateProductFiller(date: GetRandomDateTimeOffset).Create();
        private static DateTimeOffset GetRandomDateTimeOffset =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Product> CreateProductFiller(DateTimeOffset date)
        {
            var filler = new Filler<Product>();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
