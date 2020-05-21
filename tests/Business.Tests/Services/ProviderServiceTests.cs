using Business.Tests.Fixtures;
using FluentAssertions;
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
    public class ProviderServiceTests
    {
        private readonly ProviderFixture _providerTestsFixture;
        private readonly IProviderService _providerService;
        private readonly Mock<IProviderRepository> _providerRepositoryMock;
        private readonly Mock<IAddressRepository> _addressRepositoryMock;

        public ProviderServiceTests(ProviderFixture productTestsFixture)
        {
            _providerTestsFixture = productTestsFixture;
            _providerService = _providerTestsFixture.GetProviderService();
            _providerRepositoryMock = _providerTestsFixture.AutoMocker.GetMock<IProviderRepository>();
            _addressRepositoryMock = _providerTestsFixture.AutoMocker.GetMock<IAddressRepository>();
        }

        [Fact]
        public async void FindAll_ShouldReturnAListOfProvidersWithItsAddress()
        {
            // Arrange
            _providerRepositoryMock.Setup(r => r.FindAll(true))
                .Returns(Task.FromResult(_providerTestsFixture.GetValidProviders()));

            // Act
            var result = await _providerService.FindAll(true);

            // Assert

            _providerRepositoryMock.Verify(x => x.FindAll(true), Times.Once);
            result.Should().BeOfType(typeof(List<Provider>));
            result.ForEach(x => x.Address.Should().NotBeNull());
        }

        [Fact]
        public async void FindById_ShouldReturnAProvider()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var provider = _providerTestsFixture.GetValidProvider();
            _providerRepositoryMock.Setup(r => r.FindById(productId, true, true))
                .Returns(Task.FromResult(provider));

            // Act
            var result = await _providerService.FindById(productId, true, true);

            // Assert
            _providerRepositoryMock.Verify(x => x.FindById(productId, true, true), Times.Once);
            result.Should().BeEquivalentTo(provider);
        }

        [Fact]
        public async void FindAddressById_ShouldReturnAnAddress()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _addressRepositoryMock.Setup(r => r.FindById(productId))
                .Returns(Task.FromResult(_providerTestsFixture.GetAddress(productId)));

            // Act
            var result = await _providerService.FindAddressById(productId);

            // Assert
            _addressRepositoryMock.Verify(x => x.FindById(productId), Times.Once);
            result.Should().BeOfType(typeof(Address));
        }

        [Fact]
        public async void Insert_ShouldInsertAValidProvider()
        {
            // Arrange
            var provider = _providerTestsFixture.GetValidProvider();
            _providerRepositoryMock.Setup(r => r.FindById(provider.Id))
                .Returns(Task.FromResult(provider));
            _providerRepositoryMock.Setup(r => r.Search(p => p.DocumentNumber.Equals(provider.DocumentNumber) && !p.Id.Equals(provider.Id)))
                .Returns(Task.FromResult(new List<Provider>()));

            // Act
            var result = await _providerService.Insert(provider);

            // Assert
            _providerRepositoryMock.Verify(x => x.Insert(provider), Times.Once);
            _providerRepositoryMock.Verify(x => x.FindById(provider.Id), Times.Once);
            result.Should().BeEquivalentTo(provider);
        }

        [Fact]
        public async void Insert_ShouldInsertAValidProviderIfCpfAlreadyRegistred()
        {
            // Arrange
            var provider = _providerTestsFixture.GetValidProvider();
            _providerRepositoryMock.Setup(r => r.Search(p => p.DocumentNumber.Equals(provider.DocumentNumber) && !p.Id.Equals(provider.Id)))
                .Returns(Task.FromResult(new List<Provider>() { provider }));

            // Act
            var result = await _providerService.Insert(provider);

            // Assert
            _providerRepositoryMock.Verify(x => x.Insert(provider), Times.Never);
            _providerRepositoryMock.Verify(x => x.FindById(provider.Id), Times.Never);
            result.Should().BeNull();
        }

        [Fact]
        public async void Insert_ShouldNotInsertAInvalidProvider()
        {
            // Arrange
            var product = _providerTestsFixture.GetInvalidProvider();

            // Act
            var result = await _providerService.Insert(product);

            // Assert
            _providerRepositoryMock.Verify(x => x.Insert(product), Times.Never);
            result.Should().BeNull();
        }

        [Fact]
        public async void Update_ShouldUpdateAValidProduct()
        {
            // Arrange
            var provider = _providerTestsFixture.GetValidProvider();
            _providerRepositoryMock.Setup(r => r.FindById(provider.Id))
                .Returns(Task.FromResult(provider));
            _providerRepositoryMock.Setup(r => r.Search(p => p.DocumentNumber.Equals(provider.DocumentNumber) && !p.Id.Equals(provider.Id)))
                .Returns(Task.FromResult(new List<Provider>()));

            // Act
            var result = await _providerService.Update(provider);

            // Assert
            _providerRepositoryMock.Verify(x => x.Update(provider), Times.Once);
            _providerRepositoryMock.Verify(x => x.FindById(provider.Id), Times.Once);
            result.Should().BeEquivalentTo(provider);
        }

        [Fact]
        public async void Insert_ShouldNotUpdateAInvalidProvider()
        {
            // Arrange
            var provider = _providerTestsFixture.GetInvalidProvider();

            // Act
            var result = await _providerService.Update(provider);

            // Assert
            _providerRepositoryMock.Verify(x => x.Update(provider), Times.Never);
            result.Should().BeNull();
        }

        [Fact]
        public async void Update_ShouldUpdateAValidAddress()
        {
            // Arrange
            var address = _providerTestsFixture.GetAddress(Guid.NewGuid());
            _addressRepositoryMock.Setup(r => r.FindById(address.Id))
                .Returns(Task.FromResult(address));

            // Act
            var result = await _providerService.UpdateAddress(address);

            // Assert
            _addressRepositoryMock.Verify(x => x.Update(address), Times.Once);
            _addressRepositoryMock.Verify(x => x.FindById(address.Id), Times.Once);
            result.Should().BeEquivalentTo(address);
        }

        [Fact]
        public async void Remove_ShouldDeleteAProvider()
        {
            // Arrange
            var providerId = Guid.NewGuid();

            // Act
            await _providerService.Remove(providerId);

            // Assert
            _providerRepositoryMock.Verify(r => r.Remove(providerId), Times.Once);
        }
    }
}
