using Business.Tests.Fixtures;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Interfaces.Services;
using WebApiProductsProviders.Business.Models;
using Xunit;

namespace Business.Tests.Services
{
    [Collection("ServicesCollectionTests")]
    public class ProductServiceTests
    {
        private readonly ProductFixture _productTestsFixture;
        private readonly IProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;

        public ProductServiceTests(ProductFixture productTestsFixture)
        {
            _productTestsFixture = productTestsFixture;
            _productService = _productTestsFixture.GetProductService();
            _productRepositoryMock = _productTestsFixture.AutoMocker.GetMock<IProductRepository>();
        }

        [Fact]
        public async void FindAll_ShouldReturnAListOfProductsWithoutItsProvidersAndCategories()
        {
            // Arrange
            _productRepositoryMock.Setup(r => r.FindAll(false, false))
                .Returns(Task.FromResult(_productTestsFixture.GetValidProducts(false)));

            // Act
            var result = await _productService.FindAll(false, false);

            // Assert

            _productRepositoryMock.Verify(x => x.FindAll(false, false), Times.Once);
            result.Should().BeOfType(typeof(List<Product>));
            result.ForEach(x => x.Category.Should().BeNull());
            result.ForEach(x => x.Provider.Should().BeNull());
            _productService.ExecutionTimeOf(x => x.FindAll(false, false))
                .Should().BeLessThan(10.Milliseconds());
        }

        [Fact]
        public async void FindAll_ShouldReturnAListOfProductsWithItsProvidersAndCategories()
        {
            // Arrange
            _productRepositoryMock.Setup(r => r.FindAll(true, true))
                .Returns(Task.FromResult(_productTestsFixture.GetValidProducts(true)));

            // Act
            var result = await _productService.FindAll(true, true);

            // Assert

            _productRepositoryMock.Verify(x => x.FindAll(true, true), Times.Once);
            result.Should().BeOfType(typeof(List<Product>));
            result.ForEach(x => x.Category.Should().NotBeNull());
            //result.ForEach(x => x.Provider.Should().NotBeNull());
        }

        [Fact]
        public async void FindById_ShouldReturnAProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _productRepositoryMock.Setup(r => r.FindById(productId, true, true))
                .Returns(Task.FromResult(_productTestsFixture.GetValidProduct()));

            // Act
            var result = await _productService.FindById(productId, true, true);

            // Assert
            _productRepositoryMock.Verify(x => x.FindById(productId, true, true), Times.Once);
            result.Should().BeOfType(typeof(Product));
        }

        [Fact]
        public async void Insert_ShouldInsertAValidProduct()
        {
            // Arrange
            var product = _productTestsFixture.GetValidProduct();
            _productRepositoryMock.Setup(r => r.FindById(product.Id))
                .Returns(Task.FromResult(product));

            // Act
            var result = await _productService.Insert(product);

            // Assert
            _productRepositoryMock.Verify(x => x.Insert(product), Times.Once);
            _productRepositoryMock.Verify(x => x.FindById(product.Id), Times.Once);
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async void Insert_ShouldNotInsertAInvalidCategory()
        {
            // Arrange
            var category = _productTestsFixture.GetInvalidProduct();

            // Act
            var result = await _productService.Insert(category);

            // Assert
            _productRepositoryMock.Verify(x => x.Insert(category), Times.Never);
            result.Should().BeNull();
        }

        [Fact]
        public async void Update_ShouldUpdateAValidProduct()
        {
            // Arrange
            var product = _productTestsFixture.GetValidProduct();
            _productRepositoryMock.Setup(r => r.FindById(product.Id))
                .Returns(Task.FromResult(product));

            // Act
            var result = await _productService.Update(product);

            // Assert
            _productRepositoryMock.Verify(x => x.Update(product), Times.Once);
            _productRepositoryMock.Verify(x => x.FindById(product.Id), Times.Once);
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async void Insert_ShouldNotUpdateAInvalidProduct()
        {
            // Arrange
            var product = _productTestsFixture.GetInvalidProduct();

            // Act
            var result = await _productService.Update(product);

            // Assert
            _productRepositoryMock.Verify(x => x.Update(product), Times.Never);
            result.Should().BeNull();
        }

        [Fact]
        public async void Remove_ShouldDeleteAProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            await _productService.Remove(productId);

            // Assert
            _productRepositoryMock.Verify(r => r.Remove(productId), Times.Once);
        }
    }
}
