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
            return await _providerRepository.FindAll(address);
        }

        public async Task<Provider> FindById(Guid id, bool address, bool product)
        {
            return await _providerRepository.FindById(id, address, product);
        }

        public async Task Insert(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider) || !ExecuteValidation(new AddressValidation(), provider.Address)) return;

            if (_providerRepository.Search(p => p.DocumentNumber == provider.DocumentNumber).Result.Any())
            {
                Notify("CNPJ já cadastrado");
                return;
            }

            await _providerRepository.Insert(provider);
        }

        public async Task Remove(Guid id)
        {
            await _providerRepository.Remove(id);
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider)) return;

            if (_providerRepository.Search(p => p.DocumentNumber == provider.DocumentNumber && p.Id != provider.Id).Result.Any())
            {
                Notify("CNPJ já cadastrado");
                return;
            }

            await _providerRepository.Update(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;


            await _addressRepository.Update(address);
        }

        public void Dispose()
        {
            _addressRepository?.Dispose();
            _providerRepository?.Dispose();
        }
    }
}