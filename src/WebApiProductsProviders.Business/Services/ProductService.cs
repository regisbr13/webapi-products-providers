using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Interfaces.Services;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Business.Models.Validations;

namespace WebApiProductsProviders.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> FindAll(bool provider, bool category)
        {
            return await _productRepository.FindAll(provider, category);
        }

        public async Task<Product> FindById(Guid id, bool provider, bool category)
        {
            return await _productRepository.FindById(id, provider, category);
        }

        public async Task<List<Product>> FindByProvider(Guid providerId)
        {
            return await _productRepository.FindByProvider(providerId);
        }

        public async Task Insert(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            product.Register = DateTime.Now;
            await _productRepository.Insert(product);
        }

        public async Task Remove(Guid id)
        {
            await _productRepository.Remove(id);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
