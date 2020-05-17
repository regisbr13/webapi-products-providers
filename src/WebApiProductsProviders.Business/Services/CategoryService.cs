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
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, INotifier notifier) : base(notifier)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> FindAll()
        {
            return (await _categoryRepository.FindAll()).OrderBy(x => x.Name).ToList();
        }

        public async Task<Category> FindById(Guid id, bool products)
        {
            return await _categoryRepository.FindById(id, products);
        }

        public async Task<Category> Insert(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return null;

            await _categoryRepository.Insert(category);
            return await _categoryRepository.FindById(category.Id);
        }

        public async Task<Category> Update(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return null;

            await _categoryRepository.Update(category);
            return await _categoryRepository.FindById(category.Id);
        }

        public async Task Remove(Guid id)
        {
            await _categoryRepository.Remove(id);
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }
    }
}
