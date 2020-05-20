using Business.Tests.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Business.Services;
using Xunit;

namespace Business.Tests.Services
{
    [Collection(nameof(CategoryFixture))]
    public class CategoryServiceTests
    {
        private readonly CategoryFixture _categoryTestsFixture;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<INotifier> _notifierMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests(CategoryFixture categoryTestsFixture)
        {
            _categoryTestsFixture = categoryTestsFixture;
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _notifierMock = new Mock<INotifier>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _notifierMock.Object);
        }

        [Fact]
        public async void FindAll_ShouldReturnAListOfCategories()
        {
            // Arrange
            _categoryRepositoryMock.Setup(r => r.FindAll())
                .Returns(Task.FromResult(_categoryTestsFixture.GenerateValidCategories()));

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
                .Returns(Task.FromResult(_categoryTestsFixture.GenerateAValidCategory()));

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
            var category = _categoryTestsFixture.GenerateAValidCategory();
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
            var category = _categoryTestsFixture.GenerateInvalidCategory();

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
            var category = _categoryTestsFixture.GenerateAValidCategory();
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
            var category = _categoryTestsFixture.GenerateInvalidCategory();

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
