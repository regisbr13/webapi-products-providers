using Business.Tests.Fixtures;
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
    [Collection(nameof(CategoryFixture))]
    public class CategoryServiceTests
    {
        private readonly CategoryFixture _categoryTestsFixture;
        private readonly ICategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public CategoryServiceTests(CategoryFixture categoryTestsFixture)
        {
            _categoryTestsFixture = categoryTestsFixture;
            _categoryService = _categoryTestsFixture.GetCategoryService();
            _categoryRepositoryMock = _categoryTestsFixture.AutoMocker.GetMock<ICategoryRepository>();
        }

        [Fact]
        public async void FindAll_ShouldReturnAListOfCategories()
        {
            // Arrange
            _categoryRepositoryMock.Setup(r => r.FindAll())
                .Returns(Task.FromResult(_categoryTestsFixture.GetValidCategories()));

            // Act
            var categories = await _categoryService.FindAll();

            // Assert
            _categoryRepositoryMock.Verify(x => x.FindAll(), Times.Once);
            Assert.IsType<List<Category>>(categories);
        }

        [Fact]
        public async void FindById_ShouldReturnACategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _categoryRepositoryMock.Setup(r => r.FindById(categoryId, false))
                .Returns(Task.FromResult(_categoryTestsFixture.GetValidCategory()));

            // Act
            var category = await _categoryService.FindById(categoryId, false);

            // Assert
            _categoryRepositoryMock.Verify(x => x.FindById(categoryId, false), Times.Once);
            Assert.IsType<Category>(category);
        }

        [Fact]
        public async void Insert_ShouldInsertAValidCategory()
        {
            // Arrange
            var category = _categoryTestsFixture.GetValidCategory();
            _categoryRepositoryMock.Setup(r => r.FindById(category.Id))
                .Returns(Task.FromResult(category));

            // Act
            var result = await _categoryService.Insert(category);

            // Assert
            _categoryRepositoryMock.Verify(x => x.Insert(category), Times.Once);
            _categoryRepositoryMock.Verify(x => x.FindById(category.Id), Times.Once);
            Assert.IsType<Category>(result);
        }

        [Fact]
        public async void Insert_ShouldNotInsertAInvalidCategory()
        {
            // Arrange
            var category = _categoryTestsFixture.GetInvalidCategory();

            // Act
            var result = await _categoryService.Insert(category);

            // Assert
            _categoryRepositoryMock.Verify(x => x.Insert(category), Times.Never);
            Assert.Null(result);
        }

        [Fact]
        public async void Update_ShouldUpdateAValidCategory()
        {
            // Arrange
            var category = _categoryTestsFixture.GetValidCategory();
            _categoryRepositoryMock.Setup(r => r.FindById(category.Id))
                .Returns(Task.FromResult(category));

            // Act
            var result = await _categoryService.Update(category);

            // Assert
            _categoryRepositoryMock.Verify(x => x.Update(category), Times.Once);
            _categoryRepositoryMock.Verify(x => x.FindById(category.Id), Times.Once);
            Assert.IsType<Category>(result);
        }

        [Fact]
        public async void Insert_ShouldNotUpdateAInvalidCategory()
        {
            // Arrange
            var category = _categoryTestsFixture.GetInvalidCategory();

            // Act
            var result = await _categoryService.Update(category);

            // Assert
            _categoryRepositoryMock.Verify(x => x.Update(category), Times.Never);
            Assert.Null(result);
        }

        [Fact]
        public async void Remove_ShouldDeleteACategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid();

            // Act
            await _categoryService.Remove(categoryId);

            // Assert
            _categoryRepositoryMock.Verify(r => r.Remove(categoryId), Times.Once);
        }
    }
}
