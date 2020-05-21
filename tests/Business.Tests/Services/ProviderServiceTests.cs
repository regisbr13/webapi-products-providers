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

        public ProviderServiceTests(ProviderFixture providerTestsFixture)
        {
            _providerTestsFixture = providerTestsFixture;
            _providerService = _providerTestsFixture.GetProviderService();
            _providerRepositoryMock = _providerTestsFixture.AutoMocker.GetMock<IProviderRepository>();
            _addressRepositoryMock = _providerTestsFixture.AutoMocker.GetMock<IAddressRepository>();
        }

        [Fact]
        public async void FindAll_ShouldReturnAListOfProvidersWithItsAddress()
        {
            // Arrange
            var providers = _providerTestsFixture.GetValidProviders();
            _providerRepositoryMock.Setup(r => r.FindAll(true))
                .Returns(Task.FromResult(providers));

            // Act
            var result = await _providerService.FindAll(true);

            // Assert

            _providerRepositoryMock.Verify(x => x.FindAll(true), Times.Once);
            result.Should().BeEquivalentTo(providers);
            result.ForEach(x => x.Address.Should().NotBeNull());
        }

        [Fact]
        public async void FindById_ShouldReturnAProvider()
        {
            // Arrange
            var provider = _providerTestsFixture.GetValidProvider();
            _providerRepositoryMock.Setup(r => r.FindById(provider.Id, true, true))
                .Returns(Task.FromResult(provider));

            // Act
            var result = await _providerService.FindById(provider.Id, true, true);

            // Assert
            _providerRepositoryMock.Verify(x => x.FindById(provider.Id, true, true), Times.Once);
            result.Should().BeEquivalentTo(provider);
        }

        [Fact]
        public async void FindAddressById_ShouldReturnAnAddress()
        {
            // Arrange
            var address = _providerTestsFixture.GetAddress(Guid.NewGuid());
            _addressRepositoryMock.Setup(r => r.FindById(address.ProviderId))
                .Returns(Task.FromResult(address));

            // Act
            var result = await _providerService.FindAddressById(address.ProviderId);

            // Assert
            _addressRepositoryMock.Verify(x => x.FindById(address.ProviderId), Times.Once);
            result.Should().BeEquivalentTo(address);
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
            var provider = _providerTestsFixture.GetInvalidProvider();

            // Act
            var result = await _providerService.Insert(provider);

            // Assert
            _providerRepositoryMock.Verify(x => x.Insert(provider), Times.Never);
            result.Should().BeNull();
        }

        [Fact]
        public async void Update_ShouldUpdateAValidProvider()
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
