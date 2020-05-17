using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Interfaces.Services;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Business.Models.Validations;

namespace WebApiProductsProviders.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }

        public async Task<List<Provider>> FindAll(bool address)
        {
            return (await _providerRepository.FindAll(address)).OrderBy(x => x.Name).ToList();
        }

        public async Task<Provider> FindById(Guid id, bool address, bool product)
        {
            return await _providerRepository.FindById(id, address, product);
        }

        public async Task<Address> FindAddressById(Guid id)
        {
            return await _addressRepository.FindById(id);
        }

        public async Task<Provider> Insert(Provider provider)
        {
            provider.DocumentNumber = CpfClear(provider.DocumentNumber);
            if (!ExecuteValidation(new ProviderValidation(), provider) || !ExecuteValidation(new AddressValidation(), provider.Address)) return null;

            if (await CpfAlreadyRegistred(provider)) return null;

            await _providerRepository.Insert(provider);
            return await _providerRepository.FindById(provider.Id);
        }

        public async Task Remove(Guid id)
        {
            await _providerRepository.Remove(id);
        }

        public async Task<Provider> Update(Provider provider)
        {
            provider.DocumentNumber = CpfClear(provider.DocumentNumber);
            if (!ExecuteValidation(new ProviderValidation(), provider)) return null;

            if (await CpfAlreadyRegistred(provider)) return null;

            await _providerRepository.Update(provider);
            return await _providerRepository.FindById(provider.Id);
        }

        public async Task<Address> UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return null;

            await _addressRepository.Update(address);
            return await _addressRepository.FindById(address.Id);
        }

        private async Task<bool> CpfAlreadyRegistred(Provider provider)
        {
            var providers = await _providerRepository.Search(p => p.DocumentNumber.Equals(provider.DocumentNumber) && !p.Id.Equals(provider.Id));
            if (providers.Count == 0) return false;

            Notify("Cpf/Cnpj já cadastrado");
            return true;
        }

        private static string CpfClear(string cpf)
        {
            return cpf.Replace("-", "").Replace("/", "").Replace(".", "").Trim();
        }

        public void Dispose()
        {
            _addressRepository?.Dispose();
            _providerRepository?.Dispose();
        }
    }
}