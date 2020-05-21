using Bogus;
using Bogus.Extensions.Brazil;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Business.Models.Enums;
using WebApiProductsProviders.Business.Services;

namespace Business.Tests.Fixtures
{
    public class ProviderFixture : IDisposable
    {
        public AutoMocker AutoMocker;
        private readonly ProductFixture _productFixture = new ProductFixture();

        public List<Provider> GetValidProviders()
        {
            var providers = new Faker<Provider>("pt_BR")
                .CustomInstantiator(f => new Provider()
                {
                    Id = Guid.NewGuid(),
                    Name = f.Company.CompanyName(),
                    Active = f.Random.Bool(),
                    DocumentNumber = f.Random.Bool() ? f.Company.Cnpj() : f.Person.Cpf(),
                    Products = _productFixture.GetValidProducts(false)
                });
            providers.RuleFor(p => p.Address, (f, p) => GetAddress(p.Id));
            providers.RuleFor(p => p.ProviderType, (f, p) => p.DocumentNumber.Length.Equals(18) ? ProviderType.LegalPerson : ProviderType.PhysicalPerson);

            return providers.Generate(10);
        }

        public Provider GetValidProvider()
        {
            return GetValidProviders().FirstOrDefault();
        }

        public Provider GetInvalidProvider()
        {
            var provider = GetValidProvider();
            var faker = new Faker();
            provider.Name = faker.Random.Bool() ? string.Empty : provider.Name.Substring(0, 2);
            provider.DocumentNumber = provider.DocumentNumber.Substring(1);
            
            return provider;
        }

        public ProviderService GetProviderService()
        {
            AutoMocker = new AutoMocker();
            var providerService = AutoMocker.CreateInstance<ProviderService>();
            return providerService;
        }

        public Address GetAddress(Guid providerId)
        {
            return new Faker<Address>("pt_BR")
                .CustomInstantiator(f => new Address()
                {
                    Id = Guid.NewGuid(),
                    Cep = f.Address.ZipCode("########"),
                    City = f.Address.City(),
                    Complement = f.Address.SecondaryAddress(),
                    District = f.Address.State(),
                    Number = int.Parse(f.Address.BuildingNumber()),
                    State = f.Address.StateAbbr(),
                    Street = f.Address.StreetName(),
                    ProviderId = providerId
                });
        }

        public void Dispose()
        {
        }
    }
}
