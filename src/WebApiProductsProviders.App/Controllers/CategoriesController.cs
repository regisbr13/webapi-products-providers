using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.App.DTOs;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Interfaces.Services;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Data.Exceptions;

namespace WebApiProductsProviders.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("{page:int}/{pageSize:int}")]
        public async Task<ActionResult<List<CategoryDTO>>> Get(int page, int pageSize)
        {
            var categories = _mapper.Map<List<CategoryDTO>>(await _categoryService.FindAll(page, pageSize));
            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDTO>> Get(Guid id)
        {
            var categoryDTO = _mapper.Map<CategoryDTO>(await _categoryService.FindById(id, true));
            if (categoryDTO == null) return NotFound();

            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelErrors(ModelState);
                return CustomResponse();
            }

            var category = await _categoryService.Insert(_mapper.Map<Category>(categoryDTO));
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryDTO>> Put(Guid id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                NotifyError("Id's não correspondem");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                NotifyModelErrors(ModelState);
                return CustomResponse();
            }

            var category = await _categoryService.Update(_mapper.Map<Category>(categoryDTO));
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var category = await _categoryService.FindById(id, false);
                if (category == null) return NotFound();

                await _categoryService.Remove(id);
                return CustomResponse();
            }
            catch (IntegrityException exception)
            {
                return NotifyException(exception, "Existem produtos associados a esta categoria");
            }
        }
    }
}
